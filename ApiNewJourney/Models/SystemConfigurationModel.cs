﻿namespace ApiNewJourney.Models
{
    public class MessageResultModel
    {
        public bool Success { get; set; } = false;
        public string Message { get; set; }
        public object Data { get; set; } = null;
    }
}
