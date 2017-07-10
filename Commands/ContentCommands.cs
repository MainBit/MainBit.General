using System;
using System.Linq;
using Orchard.Commands;
using Orchard.ContentManagement;
using Orchard.ContentManagement.Aspects;
using Orchard.Core.Common.Models;
using Orchard.Core.Navigation.Models;
using Orchard.Core.Navigation.Services;
using Orchard.Security;
using Orchard.Settings;
using Orchard.Widgets.Models;
using Orchard.Widgets.Services;

namespace Orchard.Widgets.Commands {
    public class ContentCommands : DefaultOrchardCommandHandler {
        private readonly IContentManager _contentManager;
        public ContentCommands(
            IContentManager contentManager) {
            _contentManager = contentManager;
        }

        [CommandName("content remove")]
        [CommandHelp("content remove <identifier>\r\n\t" + "Remove a content item")]
        public void Remove(string identifier) {

            var identityPart = _contentManager
                .Query<IdentityPart, IdentityPartRecord>()
                .Where(p => p.Identifier == identifier)
                .Slice(0, 1)
                .FirstOrDefault();

            if(identityPart == null)
            {
                Context.Output.WriteLine(T("Removing content failed : content with identifier {0} was not found.", identifier));
                return;
            }

            _contentManager.Remove(identityPart.ContentItem);
            Context.Output.WriteLine(T("Content with identifier {0} and id {1} was removed successfully.").Text,
                identityPart.Identifier,
                identityPart.ContentItem.Id);
        }
    }
}