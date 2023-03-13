using System;
using System.Text;
using RabbitMQ.Client;
using BookProject.Data.Entities;

namespace BookProject.Data
{
    public class UserMessageSender
    {
        public void SendUserAddedMessage(User user)
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };

            using(var connection = factory.CreateConnection())
            using(var channel = connection.CreateModel())
            {

                channel.QueueDeclare(queue: "user-added-queue",
                                     durable: false,
                                     exclusive: false,
                                     autoDelete: false,
                                     arguments: null);

                string message = $"User Added: id={user.Id}, FirstName={user.FirstName}, LastName={user.LastName}, Email={user.Email}";
                var body = Encoding.UTF8.GetBytes(message);

                channel.BasicPublish(exchange: "",
                                     routingKey: "user-added-queue",
                                     basicProperties: null,
                                     body: body);

                Console.WriteLine("Sent {0}", message);
            }
        }
        public void SendUserUpdatedMessage(User user)
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };
            using(var connection = factory.CreateConnection())
            using(var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: "user-updated-queue",
                    durable: false,
                    exclusive: false,
                    autoDelete: false,
                    arguments: null);

                string message = $"User Updated: id={user.Id}, FirstName={user.FirstName}, LastName={user.LastName}, Email={user.Email}";
                var body = Encoding.UTF8.GetBytes(message);

                channel.BasicPublish(exchange: "",
                    routingKey: "user-updated-queue",
                    basicProperties: null,
                    body: body
                    );

                Console.WriteLine("Sent{0} ",message);

            }
        }
        public void SendUserDeletedMessage(User user)
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };
            using(var connection = factory.CreateConnection())
            using(var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: "user-deleted-queue",
                                     durable: false,
                                     exclusive: false,
                                     autoDelete: false,
                                     arguments: null);
                string message = $"User Deleted: id={user.Id}, FirstName={user.FirstName}, LastName={user.LastName}, Email={user.Email}";
                var body = Encoding.UTF8.GetBytes(message);
                channel.BasicPublish(exchange: "",
                                     routingKey: "user-deleted-queue",
                                     basicProperties: null,
                                     body: body);
                Console.WriteLine("Sent {0} ", message);
            }
        }

    }
}
