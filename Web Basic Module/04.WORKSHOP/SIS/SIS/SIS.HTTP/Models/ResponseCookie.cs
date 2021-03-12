using SIS.HTTP.Enumerators;
using System;
using System.Text;

namespace SIS.HTTP.Models
{
    public class ResponseCookie : Cookie
    {
        public ResponseCookie(string name, string value)
            : base(name, value)
        {
            this.Path = "/";
            this.Expires = DateTime.UtcNow.AddDays(30);
            this.SameSite = SameSiteType.None;
        }
        public string Domain { get; set; }

        public string Path { get; set; }

        public DateTime? Expires { get; set; }

        public long? MaxAge { get; set; }

        public bool Secure { get; set; }

        public bool HttpOnly { get; set; }

        public SameSiteType SameSite { get; set; }

        public override string ToString()
        {
            StringBuilder cb = new StringBuilder();

            cb.Append($"{this.Name}={this.Value}");
            if (this.MaxAge.HasValue)
            {
                cb.Append($"; MaxAge={this.MaxAge.Value.ToString()}");
            }
            else if (this.Expires.HasValue)
            {
                cb.Append($"; Expires={this.Expires.Value.ToString("R")}");
            }

            if (!string.IsNullOrWhiteSpace(this.Domain))
            {
                cb.Append($"; Domain={this.Domain.ToString()}");
            }

            if (!string.IsNullOrWhiteSpace(this.Path))
            {
                cb.Append($"; Path={this.Path.ToString()}");
            }

            if (this.Secure)
            {
                cb.Append($"; Secure");
            }
            if (this.HttpOnly)
            {
                cb.Append($"; HttpOnly");
            }

            cb.Append($"; SameSite={this.SameSite.ToString()}");
            return cb.ToString();
        }
    }
}
