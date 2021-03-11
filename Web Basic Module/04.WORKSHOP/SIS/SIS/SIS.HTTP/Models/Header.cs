namespace SIS.HTTP.Models
{
    public class Header
    {
        public Header(string name,string value)
        {
            this.Name = name;
            this.Value = value;
        }
        public string Name { get; set; }

        public string Value { get; set; }

        public override string ToString() => $"{this.Name}: {this.Value}";
    }
}
