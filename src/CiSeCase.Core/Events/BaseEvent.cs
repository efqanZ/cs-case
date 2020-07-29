using System;
using MediatR;

namespace CiSeCase.Core.Events
{
    public class BaseEvent : INotification
    {
        public DateTime DateOccurred { get; protected set; } = DateTime.UtcNow;
    }
}