using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;

namespace WpfDragDrop.App
{
    public class ViewModel
    {
        private readonly ObservableCollection<Model> _dragModels;
        private readonly ObservableCollection<Model> _dropModels = new ObservableCollection<Model>();

        public ViewModel()
        {
            _dragModels = new ObservableCollection<Model>
            {
                new Model { Name = "Alice" },
                new Model { Name = "Bob" },
                new Model { Name = "Charlie" },
            };
        }

        public ObservableCollection<Model> DragModels
        {
            get { return _dragModels; }
        }
        public ObservableCollection<Model> DropModels
        {
            get { return _dropModels; }
        }
    }
}
