namespace Test1
{
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
