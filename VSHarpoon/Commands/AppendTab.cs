using Microsoft.VisualStudio.Text.Formatting;
using System.Reflection;
using System.Windows.Markup;

namespace Test1
{
    [Command(PackageIds.AppendTab)]
    public sealed class AppendTab : BaseCommand<AppendTab>
    {
        protected override async Task ExecuteAsync(OleMenuCmdEventArgs e)
        {
            var activeDoc = await VS.Documents.GetActiveDocumentViewAsync();
            if (HarpoonPackage.fileNameIndexMap.Count < 10 && !HarpoonPackage.fileNameIndexMap.ContainsKey(activeDoc.FilePath))
            {
                for (int i = 0; i < HarpoonPackage.fileNamesArr.Length; i++)
                {
                    if (HarpoonPackage.fileNamesArr[i] == null) // assign to first empty block
                    {
                        HarpoonPackage.fileNamesArr[i] = activeDoc.FilePath;
                        HarpoonPackage.fileNameIndexMap.Add(activeDoc.FilePath, i);
                        Helper.UpdateLabel(i, activeDoc.FilePath);
                        break;
                    }
                }
            }
        }
    }

    [Command(PackageIds.ScrollHalfPageDown)]
    public sealed class ScrollHalfPageDown : BaseCommand<ScrollHalfPageDown>
    {
        protected override async Task ExecuteAsync(OleMenuCmdEventArgs e)
        {
            var doc = await VS.Documents.GetActiveDocumentViewAsync();
            var top = doc.TextView.ViewportTop;
            var bottom = doc.TextView.ViewportBottom;
            try
            {
                doc.TextView.ViewScroller.ScrollViewportVerticallyByPixels((top - bottom) / 2);
                var lines = doc.TextView.TextViewLines.WpfTextViewLines;

                var firstLineIndex = doc.TextView.TextViewLines.GetIndexOfTextLine(doc.TextView.TextViewLines.FirstVisibleLine);
                var lastLineIndex = doc.TextView.TextViewLines.GetIndexOfTextLine(doc.TextView.TextViewLines.LastVisibleLine);

                int midIndex = (int)((lastLineIndex - firstLineIndex)/ 2);

                if (midIndex <= lines.Count-1)
                {
                    doc.TextView.Caret.MoveTo(lines[midIndex]);
                }
                
            }
            catch (Exception ex)
            {
            }
        }
    }

    [Command(PackageIds.ScrollHalfPageUp)]
    public sealed class ScrollHalfPageUp : BaseCommand<ScrollHalfPageUp>
    {
        protected override async Task ExecuteAsync(OleMenuCmdEventArgs e)
        {
            var doc = await VS.Documents.GetActiveDocumentViewAsync();
            var top = doc.TextView.ViewportTop;
            var bottom = doc.TextView.ViewportBottom;
            try
            {
                doc.TextView.ViewScroller.ScrollViewportVerticallyByPixels((top - bottom) / -2);
                var lines = doc.TextView.TextViewLines.WpfTextViewLines;

                var firstLineIndex = doc.TextView.TextViewLines.GetIndexOfTextLine(doc.TextView.TextViewLines.FirstVisibleLine);
                var lastLineIndex = doc.TextView.TextViewLines.GetIndexOfTextLine(doc.TextView.TextViewLines.LastVisibleLine);

                int midIndex = (int)((lastLineIndex - firstLineIndex) / 2);

                if (midIndex <= lines.Count - 1)
                {
                    doc.TextView.Caret.MoveTo(lines[midIndex]);
                }
            }
            catch (Exception ex)
            {
            }
        }
    }
}
