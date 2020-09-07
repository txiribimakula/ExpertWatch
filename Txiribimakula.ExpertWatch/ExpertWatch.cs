using EnvDTE;
using EnvDTE80;
using Microsoft.VisualStudio.Shell;
using System.Runtime.InteropServices;
using Txiribimakula.ExpertWatch.ViewModels;

namespace Txiribimakula.ExpertWatch
{
    [Guid("6FD34CCE-6A7A-4016-878E-6A639BD79D69")]
    class ExpertWatch : ToolWindowPane
    {
        public ExpertWatch() : base(null) {
            this.Caption = "Expert Watch";

            DTE2 DTE2 = ExpertWatchCommand.Instance.ServiceProvider.GetService(typeof(DTE)) as DTE2;

            ViewModel viewModel = new ViewModel(DTE2.Debugger);
            BlueprintsOptionPage page = (BlueprintsOptionPage)ExpertWatchCommand.Instance.package.GetDialogPage(typeof(BlueprintsOptionPage));
            page.OptionChangedEvent += viewModel.OnToolsOptionsBlueprintsChanged;
            viewModel.OnToolsOptionsBlueprintsChanged(page.Blueprints);
            ExpertWatchWindow window = new ExpertWatchWindow();
            window.DataContext = viewModel;
            this.Content = window;
        }
    }
}
