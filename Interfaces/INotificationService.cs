using System;
using System.Collections.Generic;
using EduConnect.Models;
using EduConnect.Enums;

namespace EduConnect.Interfaces
{
    public interface INotificationService
    {
        IEnumerable<Notification> GetNotificationsForUser(Guid? userId);
        int GetUnreadCount(Guid? userId);
        
        void SendNotification(Guid? userId, string title, string message, NotificationType type);
        void BroadcastNotification(string title, string message);
        void MarkAsRead(Guid notificationId);

        // Event triggered when a new notification is created
        event Action<Notification> OnNewNotification;
    }
}
