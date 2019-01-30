using System;
using System.Collections.Generic;
using System.Text;

namespace LazZiya.ImageResize
{
    public class ImageResizeResult
    {
        public bool Success { get; set; }
        public string Value { get; set; }

        public FailureReasonType Reason { get; set; }
    }
}
