using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Routing;

namespace Examples.Pages
{
    public class IndexModel : PageModel
    {
        public class RouteMetaData
        {
            public string Type { get; set; } = string.Empty;
            public string PageRoute { get; set; }
            public string RouteTemplate { get; set; }
            public string Id { get; set; }
            public string AreaName { get; set; }
            public string DisplayName { get; set; }
            public string ViewEnginePath { get; set; }
            public string RelativePath { get; set; }
            public string RouteName { get; set; }
            public bool SuppressLinkGeneration { get; set; }
            public string Text { get; set; }
        }

        private readonly EndpointDataSource _endpointsDataSource;

        public List<BootstrapTreeNode> RouteNodesForTree { get; set; }

        public IndexModel(EndpointDataSource endpointsDataSource)
        {
            _endpointsDataSource = endpointsDataSource;
        }

        public void OnGet()
        {
            var routeMetaDatas = GetRouteMetaData();

            RouteNodesForTree = CreateBootstrapTreeNodes(routeMetaDatas);
        }

        private List<BootstrapTreeNode> CreateBootstrapTreeNodes(List<RouteMetaData> routeMetaDatas,
            int segmentIndex = 1)
        {
            //routeMetaDatas.GroupBy(x => new DirectoryInfo(x.PageRoute).Root.Name);
            var result = routeMetaDatas.GroupBy(x =>
                {
                    if (string.IsNullOrEmpty(x.PageRoute))
                    {
                        return string.Empty;
                    }

                    return new Uri("file://" + x.PageRoute).Segments[segmentIndex].Replace("/", "");
                }).Select(x =>
                {
                    var node = new BootstrapTreeNode()
                    {
                        Text = HttpUtility.UrlDecode(x.Key),
                        Id = x.First().Id,
                        Url = x.First().PageRoute,
                        PageSource = x.First().RelativePath,
                        PageModelSource = x.First().RelativePath + ".cs"
                    };

                    var children =
                        x.Where(w => !string.IsNullOrEmpty(w.PageRoute))
                            .Where(w => new Uri("file://" + w.PageRoute).Segments.Length - 1 > segmentIndex).ToList();

                    if (node.PageSource.Contains("HTTP"))
                    {
                        //Carter Route
                        node.PageSource = "/Pages" + x.First().PageRoute + ".cs";
                        node.PageModelSource = null;

                        if (!children.Any())
                        {
                            node.Methods =
                                x.Select(w => w.RelativePath.Split(" ").Last())
                                    .Where(t => t != "HEAD")
                                    .ToArray();
                        }
                    }

                    if (children.Any())
                    {
                        node.Nodes = CreateBootstrapTreeNodes(
                            children,
                            segmentIndex + 1);
                    }

                    return node;
                })
                .OrderBy(x => x.Text)
                .Where(x => !string.IsNullOrWhiteSpace(x.Text))
                .ToList();

            return result;
        }


        [DataContract]
        public class BootstrapTreeNode
        {
            [DataMember(Name = "text")] public string Text { get; set; }

            [DataMember(Name = "nodes")] public List<BootstrapTreeNode> Nodes { get; set; }

            public string Id { get; set; }
            public string Url { get; set; }
            public string PageSource { get; set; }
            public string PageModelSource { get; set; }
            public string[] Methods { get; set; }
        }

        private List<RouteMetaData> GetRouteMetaData()
        {
            List<RouteMetaData> routeMetaDatas = new List<RouteMetaData>();
            foreach (var endpoint in _endpointsDataSource.Endpoints.OfType<RouteEndpoint>())
            {
                RouteMetaData routeMetaData = new RouteMetaData();
                routeMetaDatas.Add(routeMetaData);

                routeMetaData.DisplayName = endpoint.DisplayName;
                routeMetaData.RouteTemplate = endpoint.RoutePattern.RawText;
                routeMetaData.PageRoute = endpoint.RoutePattern.RawText;
                routeMetaData.RelativePath = endpoint.DisplayName;

                foreach (var md in endpoint.Metadata)
                {
                    switch (md)
                    {
                        case PageRouteMetadata prm:
                            routeMetaData.Type += nameof(PageRouteMetadata) + " ";
                            routeMetaData.PageRoute = prm.PageRoute;
                            routeMetaData.RouteTemplate = prm.RouteTemplate;
                            break;
                        case PageActionDescriptor pad:
                            routeMetaData.Type += nameof(PageActionDescriptor) + " ";
                            routeMetaData.Id = pad.Id;
                            routeMetaData.ViewEnginePath = pad.ViewEnginePath;
                            routeMetaData.AreaName = pad.AreaName;
                            routeMetaData.DisplayName = pad.DisplayName;
                            routeMetaData.RelativePath = pad.RelativePath;
                            break;
                        case RouteNameMetadata rnm:
                            routeMetaData.Type += nameof(RouteNameMetadata) + " ";
                            routeMetaData.RouteName = rnm.RouteName;
                            break;
                        case SuppressLinkGenerationMetadata slg:
                            routeMetaData.Type += nameof(SuppressLinkGenerationMetadata) + " ";
                            routeMetaData.SuppressLinkGeneration = slg.SuppressLinkGeneration;
                            break;
                        default:
                            routeMetaData.Text = md.ToString();
                            break;
                    }
                }
            }

            return routeMetaDatas;
        }
    }
}
