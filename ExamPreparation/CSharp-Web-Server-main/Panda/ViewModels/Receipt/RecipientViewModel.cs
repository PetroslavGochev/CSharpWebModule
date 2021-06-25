namespace Panda.ViewModels.Receipt
{
    using System;

    public class RecipientViewModel
    {
        public string Id { get; set; }

        public decimal Fee { get; set; }

        public DateTime IssuedOn { get; set; }

        public string Recipient { get; set; }
    }
}
