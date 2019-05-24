using System;
using System.Collections.Generic;
using System.Text;

namespace EventSourcing
{
    public class StudentEvent:IEvent
    {
        public int EventId { get; set; }
        /// <summary>
        /// 事件类型
        /// </summary>
        public string EventType { get; set; }
        /// <summary>
        /// 事件数据
        /// </summary>
        public string EventData { get; set; }
        /// <summary>
        /// 事件描述
        /// </summary>
        public string EventDescription { get; set; }
    }
}
