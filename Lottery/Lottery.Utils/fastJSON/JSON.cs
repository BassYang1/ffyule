using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;

namespace Lottery.Utils.fastJSON
{
	public class JSON
	{
		private JSON()
		{
		}

		public string ToJSON(object obj)
		{
			return new JSONSerializer().ConvertToJSON(obj);
		}

		public object ToObject(string json)
		{
			Dictionary<string, object> dictionary = (Dictionary<string, object>)JsonParser.JsonDecode(json);
			if (dictionary == null)
			{
				return null;
			}
			return dictionary;
		}

		private Type GetTypeFromCache(string typename)
		{
			if (this._typecache.ContainsKey(typename))
			{
				return this._typecache[typename];
			}
			Type type = Type.GetType(typename);
			this._typecache.Add(typename, type);
			return type;
		}

		private PropertyInfo getproperty(Type type, string propertyname)
		{
			if (propertyname == "$type")
			{
				return null;
			}
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append(type.Name);
			stringBuilder.Append("|");
			stringBuilder.Append(propertyname);
			string key = stringBuilder.ToString();
			if (this._propertycache.ContainsKey(key))
			{
				return this._propertycache[key];
			}
			PropertyInfo[] properties = type.GetProperties();
			PropertyInfo[] array = properties;
			for (int i = 0; i < array.Length; i++)
			{
				PropertyInfo propertyInfo = array[i];
				StringBuilder stringBuilder2 = new StringBuilder();
				stringBuilder2.Append(type.Name);
				stringBuilder2.Append("|");
				stringBuilder2.Append(propertyInfo.Name);
				string key2 = stringBuilder2.ToString();
				if (!this._propertycache.ContainsKey(key2))
				{
					this._propertycache.Add(key2, propertyInfo);
				}
			}
			return this._propertycache[key];
		}

		private static JSON.GenericSetter CreateSetMethod(PropertyInfo propertyInfo)
		{
			MethodInfo setMethod = propertyInfo.GetSetMethod();
			if (setMethod == null)
			{
				return null;
			}
			Type[] array = new Type[2];
			array[0] = (array[1] = typeof(object));
			DynamicMethod dynamicMethod = new DynamicMethod("_Set" + propertyInfo.Name + "_", typeof(void), array, propertyInfo.DeclaringType);
			ILGenerator iLGenerator = dynamicMethod.GetILGenerator();
			iLGenerator.Emit(OpCodes.Ldarg_0);
			iLGenerator.Emit(OpCodes.Castclass, propertyInfo.DeclaringType);
			iLGenerator.Emit(OpCodes.Ldarg_1);
			if (propertyInfo.PropertyType.IsClass)
			{
				iLGenerator.Emit(OpCodes.Castclass, propertyInfo.PropertyType);
			}
			else
			{
				iLGenerator.Emit(OpCodes.Unbox_Any, propertyInfo.PropertyType);
			}
			iLGenerator.EmitCall(OpCodes.Callvirt, setMethod, null);
			iLGenerator.Emit(OpCodes.Ret);
			return (JSON.GenericSetter)dynamicMethod.CreateDelegate(typeof(JSON.GenericSetter));
		}

		private static JSON.GenericGetter CreateGetMethod(PropertyInfo propertyInfo)
		{
			MethodInfo getMethod = propertyInfo.GetGetMethod();
			if (getMethod == null)
			{
				return null;
			}
			Type[] parameterTypes = new Type[]
			{
				typeof(object)
			};
			DynamicMethod dynamicMethod = new DynamicMethod("_Get" + propertyInfo.Name + "_", typeof(object), parameterTypes, propertyInfo.DeclaringType);
			ILGenerator iLGenerator = dynamicMethod.GetILGenerator();
			iLGenerator.DeclareLocal(typeof(object));
			iLGenerator.Emit(OpCodes.Ldarg_0);
			iLGenerator.Emit(OpCodes.Castclass, propertyInfo.DeclaringType);
			iLGenerator.EmitCall(OpCodes.Callvirt, getMethod, null);
			if (!propertyInfo.PropertyType.IsClass)
			{
				iLGenerator.Emit(OpCodes.Box, propertyInfo.PropertyType);
			}
			iLGenerator.Emit(OpCodes.Ret);
			return (JSON.GenericGetter)dynamicMethod.CreateDelegate(typeof(JSON.GenericGetter));
		}

		internal List<Getters> GetGetters(Type type)
		{
			if (this._getterscache.ContainsKey(type))
			{
				return this._getterscache[type];
			}
			PropertyInfo[] properties = type.GetProperties(BindingFlags.Instance | BindingFlags.Public);
			List<Getters> list = new List<Getters>();
			PropertyInfo[] array = properties;
			for (int i = 0; i < array.Length; i++)
			{
				PropertyInfo propertyInfo = array[i];
				JSON.GenericGetter genericGetter = JSON.CreateGetMethod(propertyInfo);
				if (genericGetter != null)
				{
					list.Add(new Getters
					{
						Name = propertyInfo.Name,
						Getter = genericGetter
					});
				}
			}
			this._getterscache.Add(type, list);
			return list;
		}

		private JSON.GenericSetter GetSetter(PropertyInfo prop)
		{
			if (this._settercache.ContainsKey(prop))
			{
				return this._settercache[prop];
			}
			JSON.GenericSetter genericSetter = JSON.CreateSetMethod(prop);
			this._settercache.Add(prop, genericSetter);
			return genericSetter;
		}

		private object ParseDictionary(Dictionary<string, object> d)
		{
			string typename = string.Concat(d["$type"]);
			Type typeFromCache = this.GetTypeFromCache(typename);
			object obj = Activator.CreateInstance(typeFromCache);
			foreach (string current in d.Keys)
			{
				PropertyInfo propertyInfo = this.getproperty(typeFromCache, current);
				if (propertyInfo != null)
				{
					object obj2 = d[current];
					if (obj2 != null)
					{
						Type propertyType = propertyInfo.PropertyType;
						object @interface = propertyType.GetInterface("IDictionary");
						object value;
						if (propertyType.IsGenericType && !propertyType.IsValueType && @interface == null)
						{
							IList list = (IList)Activator.CreateInstance(propertyType);
							foreach (object current2 in ((ArrayList)obj2))
							{
								list.Add(this.ParseDictionary((Dictionary<string, object>)current2));
							}
							value = list;
						}
						else if (propertyType == typeof(byte[]))
						{
							value = Convert.FromBase64String((string)obj2);
						}
						else if (propertyType.IsArray && !propertyType.IsValueType)
						{
							ArrayList arrayList = new ArrayList();
							foreach (object current3 in ((ArrayList)obj2))
							{
								arrayList.Add(this.ParseDictionary((Dictionary<string, object>)current3));
							}
							value = arrayList.ToArray(propertyInfo.PropertyType.GetElementType());
						}
						else if (propertyType == typeof(Guid) || propertyType == typeof(Guid?))
						{
							value = new Guid(string.Concat(obj2));
						}
						else if (propertyType == typeof(DataSet))
						{
							value = this.CreateDataset((Dictionary<string, object>)obj2);
						}
						else if (propertyType == typeof(Hashtable))
						{
							value = this.CreateDictionary((ArrayList)obj2, propertyType);
						}
						else if (@interface != null)
						{
							value = this.CreateDictionary((ArrayList)obj2, propertyType);
						}
						else
						{
							value = this.ChangeType(obj2, propertyType);
						}
						JSON.GenericSetter setter = this.GetSetter(propertyInfo);
						setter(obj, value);
					}
				}
			}
			return obj;
		}

		private object CreateDictionary(ArrayList reader, Type pt)
		{
			IDictionary dictionary = (IDictionary)Activator.CreateInstance(pt);
			Type[] genericArguments = dictionary.GetType().GetGenericArguments();
			foreach (object current in reader)
			{
				Dictionary<string, object> dictionary2 = (Dictionary<string, object>)current;
				object key;
				if (dictionary2["k"] is Dictionary<string, object>)
				{
					key = this.ParseDictionary((Dictionary<string, object>)dictionary2["k"]);
				}
				else
				{
					key = this.ChangeType(dictionary2["k"], genericArguments[0]);
				}
				object value;
				if (dictionary2["v"] is Dictionary<string, object>)
				{
					value = this.ParseDictionary((Dictionary<string, object>)dictionary2["v"]);
				}
				else
				{
					value = this.ChangeType(dictionary2["v"], genericArguments[1]);
				}
				dictionary.Add(key, value);
			}
			return dictionary;
		}

		public object ChangeType(object value, Type conversionType)
		{
			if (conversionType.IsGenericType && conversionType.GetGenericTypeDefinition().Equals(typeof(Nullable<>)))
			{
				NullableConverter nullableConverter = new NullableConverter(conversionType);
				conversionType = nullableConverter.UnderlyingType;
			}
			return Convert.ChangeType(value, conversionType);
		}

		private Hashtable CreateHashtable(ArrayList reader)
		{
			Hashtable hashtable = new Hashtable();
			foreach (object current in reader)
			{
				Dictionary<string, object> dictionary = (Dictionary<string, object>)current;
				hashtable.Add(this.ParseDictionary((Dictionary<string, object>)dictionary["k"]), this.ParseDictionary((Dictionary<string, object>)dictionary["v"]));
			}
			return hashtable;
		}

		private DataSet CreateDataset(Dictionary<string, object> reader)
		{
			DataSet dataSet = new DataSet();
			string s = string.Concat(reader["$schema"]);
			TextReader reader2 = new StringReader(s);
			dataSet.ReadXmlSchema(reader2);
			foreach (string current in reader.Keys)
			{
				if (!(current == "$type") && !(current == "$schema"))
				{
					object obj = reader[current];
					if (obj != null && obj.GetType() == typeof(ArrayList))
					{
						ArrayList arrayList = (ArrayList)obj;
						foreach (Dictionary<string, object> dictionary in arrayList)
						{
							DataRow dataRow = dataSet.Tables[current].NewRow();
							foreach (string current2 in dictionary.Keys)
							{
								dataRow[current2] = dictionary[current2];
							}
							dataSet.Tables[current].Rows.Add(dataRow);
						}
					}
				}
			}
			return dataSet;
		}

		public static readonly JSON Instance = new JSON();

		private SafeDictionary<string, Type> _typecache = new SafeDictionary<string, Type>();

		private SafeDictionary<string, PropertyInfo> _propertycache = new SafeDictionary<string, PropertyInfo>();

		private SafeDictionary<Type, List<Getters>> _getterscache = new SafeDictionary<Type, List<Getters>>();

		private SafeDictionary<PropertyInfo, JSON.GenericSetter> _settercache = new SafeDictionary<PropertyInfo, JSON.GenericSetter>();

		private delegate void GenericSetter(object target, object value);

		public delegate object GenericGetter(object target);
	}
}
