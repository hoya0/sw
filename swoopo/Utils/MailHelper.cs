#region Disclaimer/Info
///////////////////////////////////////////////////////////////////////////////////////////////////
// .Text WebLog
// 
// .Text is an open source weblog system started by Scott Watermasysk. 
// Blog: http://ScottWater.com/blog 
// RSS: http://scottwater.com/blog/rss.aspx
// Email: Dottext@ScottWater.com
//
// For updated news and information please visit http://scottwater.com/dottext and subscribe to 
// the Rss feed @ http://scottwater.com/dottext/rss.aspx
//
// On its release (on or about August 1, 2003) this application is licensed under the BSD. However, I reserve the 
// right to change or modify this at any time. The most recent and up to date license can always be fount at:
// http://ScottWater.com/License.txt
// 
// Please direct all code related questions to:
// GotDotNet Workspace: http://www.gotdotnet.com/Community/Workspaces/workspace.aspx?id=e99fccb3-1a8c-42b5-90ee-348f6b77c407
// Yahoo Group http://groups.yahoo.com/group/DotText/
// 
///////////////////////////////////////////////////////////////////////////////////////////////////
#endregion

using System;
using System.Web;
using System.Web.Mail;
using System.Web.UI;
using OpenPOP.POP3;
using System.Reflection;
using System.IO;
using System.Xml;


namespace Swoopo.Utils
{
	/// <summary>
	/// Default implementation of the IMailProvider
	/// </summary>
	public class MailHelper 
	{
        public MailHelper() { }

        private string _adminEmail;//"master@duanke.com";
        public string AdminEmail
        {
            get { return _adminEmail; }
            set { _adminEmail = value; }
        }

        private string _smtpServer;//"mail.duanke.com";
        public string SmtpServer
        {
            get { return _smtpServer; }
            set { _smtpServer = value; }
        }

        private string _popServer;//"mail.duanke.com";
        /// <summary>
        /// ���ʼ�����
        /// </summary>
        public string PopServer
        {
            get { return _popServer; }
            set { _popServer = value; }
        }

        private string _password;// "master@duanke.com888";
        public string Password
        {
            get { return _password; }
            set { _password = value; }
        }

        private string _userName;// "master@duanke.com";
        public string UserName
        {
            get { return _userName; }
            set { _userName = value; }
        }

		public bool Send(string to, string from, string subject, string message)
		{
            //try
			{
                //string SmtpServer = "mail.duanke.cn";
                //string AdminEmail = "webmaster@duanke.cn";
                //string Password = "webmaster8888";
                //string UserName = "webmaster@duanke.cn";
                try
                {
                    ////��һ�෽����MailMessage
                    System.Web.Mail.MailMessage mailMessage = new System.Web.Mail.MailMessage();
                    mailMessage.To = to;
                    mailMessage.From = from;
                    mailMessage.Subject = subject;
                    mailMessage.Body = message.Replace("\r\n", "<br>");
                    mailMessage.BodyFormat = MailFormat.Html;//.Text;//�ʼ��ĸ�ʽ


                    //������������SMTP��Ҫ�����֤ʱ��Framework1.1����֧�֡�
                    mailMessage.Fields.Add("http://schemas.microsoft.com/cdo/configuration/smtpauthenticate", "1"); //������֤
                    mailMessage.Fields.Add("http://schemas.microsoft.com/cdo/configuration/sendusername", UserName); //�趨�û���
                    mailMessage.Fields.Add("http://schemas.microsoft.com/cdo/configuration/sendpassword", Password); //�趨����

                    SmtpMail.SmtpServer = SmtpServer;//�趨SMTP������

                    SmtpMail.Send(mailMessage);
                }
                catch (Exception)
                {
                    throw new Exception("�ʼ���������д��������ȷ����������ַ�Լ���������Ϣ����ȷ��");
                }

                //�ڶ��෽����OpenSMTP
                //string smtpHost = SmtpServer; //"smtp.163.com"; 
                //int smtpPort = 25;
                //string senderEmail = AdminEmail; //"thehim@163.com";
                //Smtp smtp = new Smtp(smtpHost, smtpPort);
                //smtp.Password = Password;//"mypass";//�û����� 
                //smtp.Username = UserName; //"thehim"; //�û�����

                ////�����ʼ���Ϣ==========================================================
                //OpenSmtp.Mail.MailMessage msg = new OpenSmtp.Mail.MailMessage();//(senderEmail, recipientEmail);
                //OpenSmtp.Mail.EmailAddress addfrom = new EmailAddress(senderEmail); //������
                //addfrom.Name = "�̿���";
                //msg.From = addfrom;

                //OpenSmtp.Mail.EmailAddress addbcc = new EmailAddress(to);
                //msg.AddRecipient(addbcc, AddressType.To);

                //msg.Subject = subject;
                //msg.Charset = "gb2312";
                //msg.Body = message;

                //smtp.SendMail(msg);


                ////�����෽����Mailserder Using
                //MailSender ms = new MailSender();
                //ms.From = AdminEmail;
                //ms.To = to;
                //ms.Subject = subject;
                //ms.Body = message;
                //ms.UserName = UserName;  // ��ô�ܸ�������
                //ms.Password = Password; // ��ô�ܸ�������
                //ms.Server = SmtpServer;

                ////ms.Attachments.Add(new MailSender.AttachmentInfo(@"D:\\test.txt"));

                //ms.SendMail();

				return true;

			}
            //catch
            //{
            //    return false;
            //}
		}

        /// <summary>
        /// �����ʼ����������д����ʼ�
        /// </summary>
        public MailResult ReceiveMail(string asmName, string typeName, string methodName ,bool delete)
        {
            MailResult result = new MailResult();
            string strPort = "";
            if (strPort == "" || strPort == string.Empty) strPort = "110";
            POPClient popClient = new POPClient();
            try
            {
                popClient.Connect(PopServer, Convert.ToInt32(strPort));
                popClient.Authenticate(UserName, Password);
                int count = popClient.GetMessageCount();
                
                result.Count = count;

                for (int i = count; i >= 1; i--)
                {
                    OpenPOP.MIMEParser.Message msg = popClient.GetMessage(i, false);
                    if (msg != null)
                    {
                        //��ȡDLL����·��:
                        string dllPath = new Uri(Assembly.GetExecutingAssembly().CodeBase).LocalPath;
                        dllPath = Path.GetDirectoryName(dllPath);
                        //������Ҫִ��������������ȡ��ʵ����
                        string asmNames = asmName;//�������ƣ�*.dll��
                        string dllFile = Path.Combine(dllPath, asmNames);
                        Assembly asm = Assembly.LoadFrom(dllFile);
                        //��ȡ�෽����ִ�У�
                        object obj = asm.CreateInstance(typeName, false);
                        Type type = obj.GetType();//����
                        MethodInfo method = type.GetMethod(methodName);//��������
                        //�����Ҫ������������
                        object[] args = new object[] { (object)msg ,(object)result};
                        //ִ�в����÷���
                        method.Invoke(obj, args);
                        if (delete)
                        {
                            popClient.DeleteMessage(i); //�ʼ�����ɹ���ɾ������������
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }  
            finally
            {
                popClient.Disconnect();

            }
            return result;
        }

	}

    public class MailResult
    {
        int count;

        public int Count
        {
            get { return count; }
            set { count = value; }
        }

        int success;

        public int Success
        {
            get { return success; }
            set { success = value; }
        }
    }
}

