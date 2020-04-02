using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace EmailDemo
{
    public class EmailHelper
    {
        #region 私有变量
        /// <summary>
        /// 发送人邮箱
        /// </summary>
        private string fromemail { get; set; }
        /// <summary>
        /// 发送人授权码
        /// </summary>
        private string authorization { get; set; }
        /// <summary>
        /// 发送人邮箱类型
        /// </summary>
        private EmailType emailtype { get; set; }
        /// <summary>
        /// 发送主机地址
        /// </summary>
        private string host { get; set; }
        #endregion

        #region 公有变量
        /// <summary>
        /// 收件人列表
        /// </summary>
        public List<string> ToEmailList = new List<string>();
        /// <summary>
        /// 邮件主题编码
        /// </summary>
        public Encoding SubjectEncoding = Encoding.UTF8;
        /// <summary>
        /// 内容编码
        /// </summary>
        public Encoding BodyEncoding = Encoding.Default;
        /// <summary>
        /// 邮件优先级
        /// </summary>
        public MailPriority Priority = MailPriority.High;
        /// <summary>
        /// 是否以Html格式发送
        /// </summary>
        public bool IsBodyHtml = true;
        /// <summary>
        /// 附件列表
        /// </summary>
        public List<Attachment> Attachments = new List<Attachment>();
        #endregion

        /// <summary>
        /// 创建邮件发送类
        /// 发送人授权码通常获取方式为：
        /// 进入邮箱设置界面 -> 找到POP3/SMTP/IMAP设置 -> 开启POP3/SMTP/IMAP服务 -> 附近可找到授权码(通常叫做客户端授权码)
        /// </summary>
        /// <param name="fromemail">发送人邮箱</param>
        /// <param name="toemail">接收人邮箱</param>
        /// <param name="authorization">发送人授权码</param>
        /// <param name="emailtype">发送人邮箱类型</param>
        public EmailHelper(string fromemail, string authorization, EmailType emailtype)
        {
            this.fromemail = fromemail;
            this.authorization = authorization;
            this.emailtype = emailtype;
            switch (emailtype)
            {
                case EmailType.QQ_PERSONAL_EMIAL:
                    host = "smtp.qq.com";
                    break;
                case EmailType.QQ_EENTERPRISE_EMAIL:
                    host = "smtp.exmail.qq.com";
                    break;
                case EmailType.NETEASE_MAIL:
                    host = "smtp.163.com";
                    break;
                case EmailType.EMAIL_126:
                    host = "smtp.126.com";
                    break;
                case EmailType.SINA_EMAIL:
                    host = "smtp.sina.cn";
                    break;
                case EmailType.EMAIL_21CN:
                    host = "smtp.21cn.com";
                    break;
                case EmailType.SOHU_EMAIL:
                    host = "smtp.sohu.com";
                    break;
                case EmailType.HOTMAIL:
                    host = "smtp.live.com";
                    break;
                case EmailType.EMAIL_139:
                    host = "smtp.139.com";
                    break;
                case EmailType.JINGAN_EMAIL:
                    host = "smtp.zzidc.com";
                    break;
                case EmailType.ALIYUAN_EMAIL:
                    host = "smtp.mxhichina.com";
                    break;
                default:
                    host = null;
                    break;
            }
        }

        /// <summary>
        /// 邮件类型
        /// </summary>
        public enum EmailType
        {
            /// <summary>
            /// QQ个人邮箱
            /// </summary>
            QQ_PERSONAL_EMIAL,

            /// <summary>
            /// QQ企业邮箱
            /// </summary>
            QQ_EENTERPRISE_EMAIL,

            /// <summary>
            /// 新浪邮箱
            /// </summary>
            SINA_EMAIL,

            /// <summary>
            /// 网易邮箱
            /// </summary>
            NETEASE_MAIL,

            /// <summary>
            /// 126邮箱
            /// </summary>
            EMAIL_126,

            /// <summary>
            /// 21CN 邮箱
            /// </summary>
            EMAIL_21CN,

            /// <summary>
            /// 搜狐邮箱
            /// </summary>
            SOHU_EMAIL,

            /// <summary>
            /// HotMail邮箱
            /// </summary>
            HOTMAIL,

            /// <summary>
            /// 移动139邮箱
            /// </summary>
            EMAIL_139,

            /// <summary>
            /// 景安网络邮箱
            /// </summary>
            JINGAN_EMAIL,

            /// <summary>
            /// 阿里云邮箱
            /// </summary>
            ALIYUAN_EMAIL

            //smtp.mxhichina.com
        }

        /// <summary>
        /// 发送邮件(通过收件人列表ToEmailList)
        /// </summary>
        /// <param name="title">标题</param>
        /// <param name="content">内容</param>
        /// <returns>是否发送成功</returns>
        public bool Send(string title, string content)
        {
            if (string.IsNullOrEmpty(host))
                throw new ArgumentNullException("host 邮件传送协议服务器未指定");

            if (ToEmailList is null || ToEmailList.Count() < 1)
                throw new ArgumentNullException("ToEmailList 收件人列表为空");

            try
            {
                MailMessage mailMessage = new MailMessage();
                mailMessage.From = new MailAddress(fromemail);

                ToEmailList.ForEach((item) =>
                {
                    mailMessage.To.Add(new MailAddress(item));
                });

                mailMessage.Subject = title;
                mailMessage.SubjectEncoding = SubjectEncoding;

                //邮件内容
                mailMessage.Body = content;
                mailMessage.BodyEncoding = BodyEncoding;

                //设置邮件优先级
                mailMessage.Priority = Priority;
                //是否以Html格式发送
                mailMessage.IsBodyHtml = IsBodyHtml;

                //添加附件
                Attachments?.ForEach((item) =>
                {
                    mailMessage.Attachments.Add(item);
                });

                //实例化一个SmtpClient类
                SmtpClient client = new SmtpClient();

                client.Host = this.host;

                //使用安全加密连接
                client.EnableSsl = true;

                //不和请求一块发送
                client.UseDefaultCredentials = false;

                //验证发件人身份(发件人的邮箱，邮箱里的生成授权码)
                client.Credentials = new NetworkCredential(fromemail, authorization);

                //发送
                client.Send(mailMessage);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
            return true;
        }

        /// <summary>
        /// 指定发送邮件        /// </summary>
        /// <param name="toemail">收件人邮箱 例如123456@.qq.com</param>
        /// <param name="title">标题</param>
        /// <param name="content">内容</param>
        /// <returns>是否发送成功</returns>
        public bool Send(string toemail, string title, string content)
        {
            if (string.IsNullOrEmpty(host))
                throw new ArgumentNullException("host 邮件传送协议服务器未指定");

            try
            {
                //实例化一个发送邮件类
                MailMessage mailMessage = new MailMessage();
                //发件人邮箱地址，方法重载不同，可以根据需求自行选择
                mailMessage.From = new MailAddress(fromemail);
                //收件人列表
                mailMessage.To.Add(new MailAddress(toemail));
                //邮件标题
                mailMessage.Subject = title;
                mailMessage.SubjectEncoding = SubjectEncoding;
                //邮件内容
                mailMessage.Body = content;
                mailMessage.BodyEncoding = BodyEncoding;
                //设置邮件优先级
                mailMessage.Priority = Priority;
                //是否以Html格式发送
                mailMessage.IsBodyHtml = IsBodyHtml;

                //添加附件
                Attachments?.ForEach((item) =>
                {
                    mailMessage.Attachments.Add(item);
                });

                //实例化一个SmtpClient类
                SmtpClient client = new SmtpClient();
                client.Host = this.host;
                //使用安全加密连接
                client.EnableSsl = true;
                //不和请求一块发送
                client.UseDefaultCredentials = false;
                //验证发件人身份(发件人的邮箱，邮箱里的生成授权码)
                client.Credentials = new NetworkCredential(fromemail, authorization);
                //发送
                client.Send(mailMessage);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
            return true;
        }

        /// <summary>
        /// 增加附件
        /// </summary>
        /// <param name="path">附件路径</param>
        public void AddNewAttachment(string path)
        {
            if (!System.IO.File.Exists(path))
                throw new ArgumentNullException($"附件不存在：{path}");

            this.Attachments.Add(new Attachment(path));
        }

        /// <summary>
        /// 添加附件列表
        /// </summary>
        /// <param name="filePathList"></param>
        public void AddAttachmentList(List<string> filePathList)
        {
            if (filePathList is null || filePathList.Count() < 1)
                throw new ArgumentNullException($"附件不可以为空");

            filePathList.ForEach((item) =>
            {
                if (!System.IO.File.Exists(item))
                    throw new ArgumentNullException($"附件不存在,地址：{item}");

                this.Attachments.Add(new Attachment(item));
            });
        }
    }
}
