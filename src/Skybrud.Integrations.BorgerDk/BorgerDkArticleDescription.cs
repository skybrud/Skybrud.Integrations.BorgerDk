using System;
using Skybrud.Integrations.BorgerDk.WebService;

namespace Skybrud.Integrations.BorgerDk {

    public class BorgerDkArticleDescription {

        public int Id { get; private set; }
        public string Title { get; private set; }
        public string Url { get; private set; }
        public DateTime LastUpdated { get; private set; }
        public DateTime PublishingDate { get; private set; }

        internal BorgerDkArticleDescription(ArticleDescription article) {
            Id = article.ArticleID;
            Title = article.ArticleTitle;
            Url = article.ArticleUrl;
            PublishingDate = article.PublishingDate;
            LastUpdated = article.LastUpdated;
        }

    }

}
