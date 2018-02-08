using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using StriveEngine;
using StriveEngine.Core;
using StriveEngine.Tcp.Server;

namespace Lottery.Utils
{
	public class YouLeTcpServerEngine
	{
		public void TcpConnection(int port)
		{
			try
			{
				if (this.tcpServerEngine == null)
				{
					this.tcpServerEngine = NetworkEngineFactory.CreateTextTcpServerEngine(port, new DefaultTextContractHelper(new string[]
					{
						"\0"
					}));
				}
				if (this.hasTcpServerEngineInitialized)
				{
					this.tcpServerEngine.ChangeListenerState(true);
				}
				else
				{
					this.InitializeTcpServerEngine();
				}
			}
			catch (Exception ex)
			{
				ex.ToString();
			}
		}

		private void InitializeTcpServerEngine()
		{
			this.tcpServerEngine.ClientCountChanged += new CbDelegate<int>(this.tcpServerEngine_ClientCountChanged);
			this.tcpServerEngine.ClientConnected += new CbDelegate<IPEndPoint>(this.tcpServerEngine_ClientConnected);
			this.tcpServerEngine.ClientDisconnected += new CbDelegate<IPEndPoint>(this.tcpServerEngine_ClientDisconnected);
			this.tcpServerEngine.MessageReceived += new CbDelegate<IPEndPoint, byte[]>(this.tcpServerEngine_MessageReceived);
			this.tcpServerEngine.Initialize();
			this.hasTcpServerEngineInitialized = true;
		}

		private void tcpServerEngine_MessageReceived(IPEndPoint client, byte[] bMsg)
		{
			string text = Encoding.UTF8.GetString(bMsg);
			text = text.Substring(0, text.Length - 1);
		}

		private void tcpServerEngine_ClientDisconnected(IPEndPoint ipe)
		{
			string.Format("{0} 下线", ipe);
		}

		private void tcpServerEngine_ClientConnected(IPEndPoint ipe)
		{
			string.Format("{0} 上线", ipe);
		}

		private void tcpServerEngine_ClientCountChanged(int count)
		{
		}

		public void SendMessage(string msg)
		{
			try
			{
				List<IPEndPoint> clientList = this.tcpServerEngine.GetClientList();
				foreach (IPEndPoint current in clientList)
				{
					byte[] bytes = Encoding.UTF8.GetBytes(msg);
					this.tcpServerEngine.SendMessageToClient(current, bytes);
				}
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		private ITcpServerEngine tcpServerEngine;

		private bool hasTcpServerEngineInitialized;
	}
}
