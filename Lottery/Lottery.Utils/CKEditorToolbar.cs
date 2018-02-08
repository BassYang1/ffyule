using System;

namespace Lottery.Utils
{
	public static class CKEditorToolbar
	{
		public static object[] Simple
		{
			get
			{
				return new object[]
				{
					new object[]
					{
						"Source",
						"-",
						"JustifyLeft",
						"JustifyCenter",
						"JustifyRight",
						"-",
						"Styles",
						"FontSize"
					},
					new object[]
					{
						"Bold",
						"Italic",
						"-",
						"NumberedList",
						"BulletedList",
						"-",
						"Link",
						"Unlink"
					}
				};
			}
		}

		public static object[] Basic
		{
			get
			{
				return new object[]
				{
					new object[]
					{
						"Bold",
						"Italic",
						"-",
						"OrderedList",
						"UnorderedList",
						"-",
						"Link",
						"Unlink"
					}
				};
			}
		}
	}
}
