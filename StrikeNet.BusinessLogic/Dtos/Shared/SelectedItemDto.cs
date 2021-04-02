using System;
using System.Collections.Generic;
using System.Text;

namespace StrikeNet.BusinessLogic.Dtos.Shared
{
    public class SelectedItemDto
    {
        public SelectedItemDto(string id, string text)
        {
            Id = id;
            Text = text;
        }

        public string Id { get; set; }

        public string Text { get; set; }
    }
}
