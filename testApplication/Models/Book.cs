using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace testApplication.Models
{
    public class Book : IValidatableObject
    {
        public int Id { get; set; }
        [DisplayName("書名")]
        [Required(ErrorMessage = "{0}は必須です。")]
        public string Title { get; set; }
        [DisplayName("価格")]
        [DataType(DataType.Currency)]
        [Range(0, 5000, ErrorMessage = "{0}は{1}～{2}以内で入力してください。")]
        public int Price { get; set; }
        [DisplayName("出版社")]
        [StringLength(20, ErrorMessage = "{0}は{1}文字以内で入力してください。")]
        public string Publisher { get; set; }
        [DisplayName("配布サンプル")]
        public bool Sample { get; set; }

        // 問題点：PostgreSqlが対応していない！！
        // C#とPostgresの架け橋となるNpgsqlが対応していないっぽい
        [Timestamp]
        public byte[] RowVersion { get; set; } 

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (this.Publisher == "フリー文庫" && this.Price > 0)
            {
                yield return new ValidationResult("フリー文庫の価格は0円でなければいけません。",
                    new[] { "Price" });
            }
        }
    }
}
