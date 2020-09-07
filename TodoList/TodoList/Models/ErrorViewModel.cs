using System;

namespace TodoList.Models
{
    public class ErrorViewModel
    {
        public string RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);

        public int ErrorCode { get; internal set; }
        public string ControllerName { get; internal set; }
        public string ActionName { get; internal set; }
        public string Text { get; internal set; }
    }
}
