using System;
using Skybrud.Integrations.BorgerDk.WebService;

namespace Skybrud.Integrations.BorgerDk {

    public class BorgerDkArticleDescription {

        public int Id { get; }

        public string Title { get; }

        public string Url { get; }

        public DateTime LastUpdated { get; }

        public DateTime PublishingDate { get; }

        internal BorgerDkArticleDescription(ArticleDescription article) {
            Id = article.ArticleID;
            Title = article.ArticleTitle;
            Url = article.ArticleUrl;
            PublishingDate = article.PublishingDate;
            LastUpdated = article.LastUpdated;
        }

    }

}