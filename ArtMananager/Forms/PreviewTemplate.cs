/* (C) 2017 Premysl Fara */

namespace ArtMananager.Forms
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.IO;
    using System.Text;

    using ArtMananager.Core;


    public class PreviewTemplate
    {
        public string PageTemplate { get; set; }

        public string AuthorTemplate { get; set; }

        public string ImageTemplate { get; set; }


        /// <summary>
        /// Loads a template from a file.
        /// </summary>
        /// <param name="templateFileName"></param>
        /// <returns></returns>
        public static PreviewTemplate Load(string templateFileName)
        {
            var template = new PreviewTemplate();

            var templateSource = File.ReadAllText(templateFileName);

            var pageTemplateSource = Helpers.GetStringBetween("--- BEGIN PAGE TEMPLATE ---", "--- END PAGE TEMPLATE ---", templateSource);
            if (pageTemplateSource[0] == null)
            {
                throw new ApplicationException("The page template source not found in the '" + (templateFileName ?? String.Empty) + "' file.");
            }

            template.PageTemplate = pageTemplateSource[0];

            var authorTemplateSource = Helpers.GetStringBetween("--- BEGIN AUTHOR TEMPLATE ---", "--- END AUTHOR TEMPLATE ---", templateSource);
            if (authorTemplateSource[0] == null)
            {
                throw new ApplicationException("The author template source not found in the '" + (templateFileName ?? String.Empty) + "' file.");
            }
            
            template.AuthorTemplate = authorTemplateSource[0];

            var imageTemplateSource = Helpers.GetStringBetween("--- BEGIN IMAGE TEMPLATE ---", "--- END IMAGE TEMPLATE ---", templateSource);
            if (imageTemplateSource[0] == null)
            {
                throw new ApplicationException("The image template source not found in the '" + (templateFileName ?? String.Empty) + "' file.");
            }

            template.ImageTemplate = imageTemplateSource[0];

            return template;
        }


        public static PreviewTemplate GetDefaultPreviewTemplate()
        {
            return new PreviewTemplate
            {
                PageTemplate = @"<html>
<head>
<meta charset=""UTF-8"">
<title>Autoři</title>
</head>    

<body>
<h1>Přehled autorů a jejich děl</h1>
${authors-list}
</body>
</html>",

                AuthorTemplate = @"<h2>${author-name}</h2>
<p>${images-list}</p>",

                ImageTemplate = @"<img src=""${image-path}"" alt=""${image-alt}"" height=""${image-height}"" width=""${image-width}"">"
            };
        }

        /// <summary>
        /// Returns a (HTML) source generated from this template.
        /// </summary>
        /// <param name="authorNames">A sorted list of author names.</param>
        /// <param name="artsList">A list of arts grouped by an author.</param>
        /// <returns>A (HTML) source generated from this template</returns>
        public string GetSource(List<string> authorNames, Dictionary<string, List<ImageInfo>> artsList, int previewImageSize)
        {
            if (String.IsNullOrEmpty(PageTemplate))
            {
                return String.Empty;
            }

            var sb = new StringBuilder(PageTemplate);

            sb.Replace("${authors-list}", GetAuthorsSource(authorNames, artsList, previewImageSize));

            return sb.ToString();
        }


        private string GetAuthorsSource(List<string> authorNames, Dictionary<string, List<ImageInfo>> artsList, int previewImageSize)
        {
            if (authorNames == null || artsList == null)
            {
                return String.Empty;
            }

            var sb = new StringBuilder();

            foreach (var authorName in authorNames)
            {
                sb.Append(GetAuthorSource(authorName, artsList[authorName], previewImageSize));
            }

            return sb.ToString();
        }


        private string GetAuthorSource(string authorName, List<ImageInfo> images, int previewImageSize)
        {
            if (String.IsNullOrEmpty(AuthorTemplate))
            {
                return String.Empty;
            }

            if (String.IsNullOrWhiteSpace(authorName))
            {
                authorName = "Unknown Author";
            }

            var sb = new StringBuilder(AuthorTemplate);

            sb.Replace("${author-name}", authorName);
            sb.Replace("${images-list}", GetImagesSource(images, previewImageSize));

            return sb.ToString();
        }


        private string GetImagesSource(List<ImageInfo> images, int previewImageSize)
        {
            if (images == null)
            {
                return String.Empty;
            }

            var sb = new StringBuilder();

            foreach (var image in images)
            {
                sb.Append(GetImageSource(image, previewImageSize));
            }

            return sb.ToString();
        }


        private string GetImageSource(ImageInfo imageInfo, int previewImageSize)
        {
            if (String.IsNullOrEmpty(ImageTemplate) || imageInfo == null)
            {
                return String.Empty;
            }
                 
            var imageSize = (imageInfo.Width > imageInfo.Height) ? imageInfo.Width : imageInfo.Height;
            var imageScale = (imageSize > previewImageSize) ? (previewImageSize / imageSize) : 1;

            var sb = new StringBuilder(ImageTemplate);

            sb.Replace("${image-path}", imageInfo.Path ?? String.Empty);
            sb.Replace("${image-alt}", imageInfo.Path ?? String.Empty);
            sb.Replace("${image-height}", ((int)(imageInfo.Height * imageScale)).ToString(CultureInfo.InvariantCulture));
            sb.Replace("${image-width}", ((int)(imageInfo.Width * imageScale)).ToString(CultureInfo.InvariantCulture));

            return sb.ToString();
        }
    }
}
