using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace myWebMail.Models
{
    public class MailItem
    {
        public string ID;

        [DataType(DataType.MultilineText)]
        public string Body { get; set; }
        public string Recipients { get; set; }
        public string Subject { get; set; }
        public string Sender { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM-dd-yyyy HH:mm tt}")]
        public DateTimeOffset? Received { get; set; }

        //The next two are used for implementing paging
        //Determines if this item is the first item or the last item in the collection
        public bool IsFirstItem { get; set; }
        public bool IsLastItem { get; set; }
    }
}