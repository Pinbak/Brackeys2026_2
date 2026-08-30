using Brackeys2026_2.Engine.Components;
using Brackeys2026_2.Engine.Resources;

namespace Brackeys2026_2.Game.Components;

public class NoteManager(TextRenderer stickyNoteText, SpriteRenderer stickyNote, Texture normalNote, Texture yellowNote) : Component
{
    private string _totalContent = "";
    private bool _isYellow;

    public void Clear()
    {
        _totalContent = "";
        stickyNoteText.Set(_totalContent);
        _isYellow = false;
        stickyNote.ChangeTexture(normalNote);
    }

    public void Add(string content, bool isUrgent)
    {
        var newString = _totalContent + "\n\n" + content;
        if (newString.Length > 300)
        {
            _totalContent = content + "\n\n";    
            _isYellow = false;
            stickyNote.ChangeTexture(normalNote);
        }
        else
        {
            _totalContent = content + "\n\n" + _totalContent;
        }
        
        stickyNoteText.Set(_totalContent);
        if (isUrgent)
        {
            stickyNote.ChangeTexture(yellowNote);
            _isYellow = true;
        }
    }
}