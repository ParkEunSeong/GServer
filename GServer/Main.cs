using log4net;
using SuperSocket.SocketBase;
using SuperSocket.SocketBase.Config;
using SuperSocket.SocketBase.Logging;
using SuperSocket.SocketBase.Protocol;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GServer
{
    public class Main
    {

        private UserServer m_serv;
        public void Initialize(int port = 9999)
        {
            //m_serv = new UserServer();
            //m_serv.NewRequestReceived += M_serv_NewRequestReceived;
            //if ( m_serv.Setup(new RootConfig(), GetConfig(), logFactory : new Log4NetLogFactory() ) )
            //{
            //    if (m_serv.Start())
            //    {
            //        Debug.Print("start");
            //    }
            //}
            m_serv = new UserServer();
            if (m_serv.Setup(new RootConfig(), GetConfig(), logFactory: new Log4NetLogFactory()))
            {
                if (!m_serv.Start())
                {
                    Console.WriteLine("Failed to start!");
                    Console.ReadKey();
                    return;
                }
            }

             
         
            m_serv.NewSessionConnected += new SessionHandler<UserSession>(appServer_NewSessionConnected);
            m_serv.NewRequestReceived += appServer_NewRequestReceived;
  

        }
         void appServer_NewSessionConnected(UserSession session)
        {
            Console.WriteLine($"The server got the connection from the client successfully");

            var count = m_serv.GetAllSessions().Count();
            Console.WriteLine("~~" + count);
            session.Send("Welcome to SuperSocket Telnet Server");
        }

         void appServer_NewSessionClosed(AppSession session, SuperSocket.SocketBase.CloseReason aaa)
        {
            Console.WriteLine($"The server loses the connection from the client" + session.SessionID + aaa.ToString());
            var count = m_serv.GetAllSessions().Count();
            Console.WriteLine(count);
        }

        //2.
         void appServer_NewRequestReceived(UserSession session, StringRequestInfo requestInfo)
        {
            Console.WriteLine(requestInfo.Key + "," + requestInfo.Body);
            session.Send(requestInfo.Body);
        }

        private static void M_serv_NewRequestReceived(UserSession session, SuperSocket.SocketBase.Protocol.StringRequestInfo requestInfo)
        {
            int K = 0;   
        }

        public ServerConfig GetConfig()
        {
            return new ServerConfig
            {
                Port = 9999,
                Ip = "Any",
                MaxConnectionNumber = 100,
                Mode = SocketMode.Tcp,
                Name = "UserServer"
            };
        }

    }
}
