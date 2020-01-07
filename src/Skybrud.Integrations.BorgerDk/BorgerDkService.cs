using System.Linq;
using Skybrud.Integrations.BorgerDk.WebService;

namespace Skybrud.Integrations.BorgerDk {

    public class BorgerDkService {

        private readonly ArticleExportClient _client;

        /// <summary>
        /// A reference to the endpoint (web service).
        /// </summary>
        public BorgerDkEndpoint Endpoint { get; set; }

        /// <summary>
        /// The selected municipality.
        /// </summary>
        public BorgerDkMunicipality Municipality { get; private set; }

        #region Constructors

        public BorgerDkService() {
            Endpoint = BorgerDkEndpoint.Default;
            Municipality = BorgerDkMunicipality.NoMunicipality;
            _client = Endpoint.GetClient();
        }

        public BorgerDkService(BorgerDkMunicipality municipality) {
            Endpoint = BorgerDkEndpoint.Default;
            Municipality = municipality ?? BorgerDkMunicipality.NoMunicipality;
            _client = Endpoint.GetClient();
        }

        public BorgerDkService(string domain) {
            Endpoint = BorgerDkEndpoint.GetFromDomain(domain);
            Municipality = BorgerDkMunicipality.NoMunicipality;
            _client = Endpoint.GetClient();
        }

        public BorgerDkService(string domain, BorgerDkMunicipality municipality) {
            Endpoint = BorgerDkEndpoint.GetFromDomain(domain);
            Municipality = municipality ?? BorgerDkMunicipality.NoMunicipality;
            _client = Endpoint.GetClient();
        }

        public BorgerDkService(BorgerDkEndpoint endpoint) {
            Endpoint = endpoint;
            Municipality = BorgerDkMunicipality.NoMunicipality;
            _client = Endpoint.GetClient();
        }

        public BorgerDkService(BorgerDkEndpoint endpoint, BorgerDkMunicipality municipality) {
            Endpoint = endpoint ?? BorgerDkEndpoint.Default;
            Municipality = municipality ?? BorgerDkMunicipality.NoMunicipality;
            _client = Endpoint.GetClient();
        }

        #endregion

        #region Member methods

        public int GetArticleIdFromUrl(string url) {
            ArticleShortDescription desc = _client.GetArticleIDByUrl(url);
            return desc?.ArticleID ?? 0;
        }

        public BorgerDkArticle GetArticleFromId(int articleId) {
            return BorgerDkArticle.GetFromArticle(
                this,
                _client.GetArticleByID(articleId, Municipality.Code == 0 ? null : (int?) Municipality.Code),
                Municipality
            );
        }

        public BorgerDkMunicipality[] GetMunicipalityList() {
            return _client.GetMunicipalityList().Select(x => new BorgerDkMunicipality(x.MunicipalityCode, x.MunicipalityName)).ToArray();
        }

        public BorgerDkArticleDescription[] GetArticleList() {
            return _client.GetAllArticles().Select(x => new BorgerDkArticleDescription(x)).ToArray();
        }

        #endregion

        #region Static methods

        public static bool IsValidUrl(string url) {
            return BorgerDkEndpoint.Values.Any(x => x.IsValidUrl(url));
        }

        public static bool IsValidDomain(string domain) {
            return BorgerDkEndpoint.Values.Any(x => x.Domain == domain);
        }

        #endregion

    }

}