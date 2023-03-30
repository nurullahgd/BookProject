using System;
using System.Text;
using RabbitMQ.Client;
using BookProject.Data.Entities;

namespace BookProject.Data.Messages
{
    public class ArticleMessageSender
    {
        public void SendArticleAddedMessage(string article)
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };

            using(var connection = factory.CreateConnection())
            using(var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: "article-added-queue",
                                     durable: false,
                                     exclusive: false,
                                     autoDelete: false,
                                     arguments: null);

                //var json = JsonConvert.SerializeObject(article);
                var body = Encoding.UTF8.GetBytes(article);

                channel.BasicPublish(exchange: "",
                                     routingKey: "article-added-queue",
                                     basicProperties: null,
                                     body: body);

                Console.WriteLine("Sent {0}", article);
            }
        }

        public void SendArticleUpdatedMessage(Article article)
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };
            using(var connection = factory.CreateConnection())
            using(var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: "article-updated-queue",
                    durable: false,
                    exclusive: false,
                    autoDelete: false,
                    arguments: null);

                string message = $"Article Updated: id={article.Id}, Title={article.Title}, Content={article.Content}, AuthorID={article.AuthorId}, MagazineID={article.MagazineId}";
                var body = Encoding.UTF8.GetBytes(message);

                channel.BasicPublish(exchange: "",
                    routingKey: "article-updated-queue",
                    basicProperties: null,
                    body: body
                    );

                Console.WriteLine("Sent{0} ", message);

            }
        }
        public void SendArticleDeletedMessage(Article article)
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };
            using(var connection = factory.CreateConnection())
            using(var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: "article-deleted-queue",
                                     durable: false,
                                     exclusive: false,
                                     autoDelete: false,
                                     arguments: null);
                string message = $"Article Deleted: id={article.Id}, Title={article.Title}, Content={article.Content}, AuthorID={article.AuthorId}, MagazineID={article.MagazineId}";
                var body = Encoding.UTF8.GetBytes(message);
                channel.BasicPublish(exchange: "",
                                     routingKey: "article-deleted-queue",
                                     basicProperties: null,
                                     body: body);
                Console.WriteLine("Sent {0} ", message);
            }
        }

    }
}
