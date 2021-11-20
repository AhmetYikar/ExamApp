using ExamApp.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HtmlAgilityPack;
using ExamApp.Web.Areas.Admin.ViewModels;

namespace ExamApp.Web.Areas.Admin.Helper
{
    public static class HtmlAgilityHelper
    {
        public static ArticlesResultModel GetLastFiveArticles(string url)
        {
            var articles = new List<ArticleViewModel>();
            var web = new HtmlWeb();

            try
            {
                var doc = web.Load(url);
                for (int i = 0; i < 5; i++)
                {
                    //href of the article from comes from "a" href attribute
                    var detailPageurl = "";
                    var node = doc.DocumentNode.SelectSingleNode($"//*[@id='app-root']/div/div[3]/div/div[2]/div/div[1]/div/div/ul/li[{i + 1}]/a");
                    if (node!=null)
                    {
                        detailPageurl = node.GetAttributeValue("href", string.Empty);
                        var articleContentResult = GetArticleContent($"https://www.wired.com{detailPageurl}", web);
                        if (articleContentResult.IsSuccess)
                        {
                            var article = new ArticleViewModel
                            {
                                Title = doc.DocumentNode.SelectSingleNode($"//*[@id='app-root']/div/div[3]/div/div[2]/div/div[1]/div/div/ul/li[{i + 1}]/div/a/h2").InnerText,
                                Content = articleContentResult.Content
                            };
                            articles.Add(article);
                        }
                        else
                        {
                            return new ArticlesResultModel { IsSuccess = false, Articles = null, ErrorMessage = articleContentResult.ErrorMessage };
                        }
                    }
                    else
                    {
                        return new ArticlesResultModel { IsSuccess = false, Articles = null, ErrorMessage = $"{url} sayfasından {i+1}. makaleye ait link alınırken bir hata oluştu!" };
                    }
                   
                }

                return new ArticlesResultModel { IsSuccess = true, Articles = articles };
            }
            catch
            {
                return new ArticlesResultModel { IsSuccess = false, Articles = null, ErrorMessage = $"{url} sayfasından makaleler listesi alınamadı!" };
            }

        }

        //Get single article content by the link 
        public static ArticleContentResultModel GetArticleContent(string url, HtmlWeb web)
        {
            var content = "";
            try
            {
                var doc = web.Load(url);

                var allPs = doc.DocumentNode.SelectNodes("//article//p").ToList();
                foreach (var p in allPs)
                {
                    if (p.ParentNode.Name != "span")
                    {
                        content += "<p>" + p.InnerText + "</p>";
                    }
                }

                return new ArticleContentResultModel { IsSuccess = true, Content = content };
            }
            catch
            {

                return new ArticleContentResultModel { IsSuccess = false, Content = null,ErrorMessage= $"{url} sayfasından makale içeriği alınırken bir hata oluştu!" };

            }

        }
    }
}
