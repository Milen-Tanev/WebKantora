using System;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using WebKantora.Web.Areas.Administration.Models.BlogViewModels;

namespace WebKantora.Web.Infrastructure
{
    public class FileHelpers
    {
        public static async Task<string> ProcessFormFile(IFormFile formFile, ModelStateDictionary modelState)
        {
            var fieldDisplayName = string.Empty;

            MemberInfo property = typeof(CreateArticleViewModel).GetProperty(formFile.Name.Substring(formFile.Name.IndexOf(".") + 1));

            if (property != null)
            {
                if (property.GetCustomAttribute(typeof(DisplayAttribute)) is DisplayAttribute displayAttribute)
                {
                    fieldDisplayName = $"{displayAttribute.Name} ";
                }
            }

            var fileName = WebUtility.HtmlEncode(Path.GetFileName(formFile.FileName));

            if (formFile.Length == 0)
            {
                modelState.AddModelError(formFile.Name, $"The {fieldDisplayName}file ({fileName}) is empty.");
            }
            else if (formFile.Length > 1048576)
            {
                modelState.AddModelError(formFile.Name, $"The {fieldDisplayName}file ({fileName}) exceeds 1 MB.");
            }
            else
            {
                try
                {
                    using (WordprocessingDocument wordDocument = WordprocessingDocument.Open(formFile.OpenReadStream(), false))
                    {
                        Body body = wordDocument.MainDocumentPart.Document.Body;

                        var text = await Task.Run(() => ConvertFile(body));

                        if (text.Length > 0)
                        {
                            return text;
                        }
                        else
                        {
                            modelState.AddModelError(formFile.Name, $"The {fieldDisplayName}file ({fileName}) is empty.");
                        }
                    }
                }
                catch (Exception ex)
                {
                    modelState.AddModelError(formFile.Name,
                                             $"The {fieldDisplayName}file ({fileName}) upload failed. " +
                                             $"Please contact the Help Desk for support. Error: {ex.Message}");
                    // Log the exception
                }
            }

            return string.Empty;
        }

        private static string ConvertFile(Body body)
        {
            StringBuilder sb = new StringBuilder();
            var paragraphs = body.Elements<Paragraph>();

            foreach (var paragraph in paragraphs)
            {
                if (string.IsNullOrEmpty(paragraph.InnerText))
                {
                    continue;
                }

                sb.Append("<p>");
                var runs = paragraph.Elements<Run>();

                foreach (var run in runs)
                {
                    var innerText = run.InnerText;

                    if (string.IsNullOrEmpty(innerText))
                    {
                        continue;
                    }
                    if (run.RunProperties != null)
                    {
                        var props = run.RunProperties;

                        foreach (var prop in props)
                        {
                            if (prop.LocalName == "b")
                            {
                                innerText = $"<strong>{innerText}</strong>";
                            }
                            else if (prop.LocalName == "i")
                            {
                                innerText = $"<em>{innerText}</em>";
                            }
                            else if (prop.LocalName == "sz")
                            {
                                innerText = $"<h3>{innerText}</h3>";
                            }
                        }
                        sb.Append(innerText);
                    }
                    else
                    {
                        sb.Append(run.InnerText);
                    }
                }

                sb.Append("</p>");
            }

            return sb.ToString();
        }
    }
}
