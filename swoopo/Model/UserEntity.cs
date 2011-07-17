using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.Practices.EnterpriseLibrary.Validation.Validators;


namespace Swoopo.Model
{
    [Serializable]
    public class UserEntity
    {
        public int ID { get; set; }

        //[Required(ErrorMessage = "用户名不能为空.")]
        [StringLengthValidator(1, 50, MessageTemplate = "用户名{3}-{5}字符")]
        public string UserName { get; set; }

        //[Required(ErrorMessage = "密码不能为空.")]
        [StringLengthValidator(1, 32, MessageTemplate = "密码{3}-{5}字符")]
        public string UserPwd { get; set; }

        [RegexValidator(@"([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})", ErrorMessage = "邮箱不合法。")]
        public string Email { get; set; }

        //[TypeConversionValidator(typeof(decimal))]
        public decimal Balance { get; set; }

        public byte UserState { get; set; }

        public byte UserType { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime CreateTime { get; set; }
    }
}
