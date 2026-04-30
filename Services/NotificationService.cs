using System;
using System.Collections.Generic;
using System.Linq;
using EduConnect.Interfaces;
using EduConnect.Models;
using EduConnect.Enums;

namespace EduConnect.Services
{
    public class NotificationService : INotificationService
    {
        private readonly IRepository<Notification> _notificationRepository;

        public event Action<Notification> OnNewNotification;

        public NotificationService(IRepository<Notification> notificationRepository)
        {
            _notificationRepository = notificationRepository;
        }

        public IEnumerable<Notification> GetNotificationsForUser(Guid? userId)
        {
            // Get user-specific notifications and broadcasts (where UserId is null)
            return _notificationRepository.Query()
                .Where(n => n.UserId == userId || n.UserId == null)
                .OrderByDescending(n => n.CreatedAt);
        }

        public int GetUnreadCount(Guid? userId)
        {
            return GetNotificationsForUser(userId).Count(n => !n.IsRead);
        }

        public void SendNotification(Guid? userId, string title, string message, NotificationType type)
        {
            var notification = new Notification
            {
                UserId = userId,
                Title = title,
                Message = message,
                Type = type,
                CreatedAt = DateTime.UtcNow,
                IsRead = false
            };

            _notificationRepository.Add(notification);
            OnNewNotification?.Invoke(notification);
        }

        public void BroadcastNotification(string title, string message)
        {
            SendNotification(null, title, message, NotificationType.General);
        }

        public void MarkAsRead(Guid notificationId)
        {
            var notification = _notificationRepository.GetById(notificationId);
            if (notification != null && !notification.IsRead)
            {
                notification.IsRead = true;
                _notificationRepository.Update(notification);
            }
        }
    }
}
