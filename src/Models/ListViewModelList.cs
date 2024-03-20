using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace LibraryManagementSystem.Models
{
    public class ListViewModelList : ObservableCollection<ListViewModel>
    {

        public new void Add(ListViewModel lvm)
        {
            base.Add(lvm);
            lvm.Id = this.IndexOf(lvm);
        }

        public new void Remove(ListViewModel lvm)
        {
            base.Remove(lvm);
            lvm.Id = -1;
            Reseed();
        }

        public new void RemoveAt(int index)
        {
            ListViewModel lvm = this[index];
            this.Remove(lvm);
        }

        public int RemoveAll(Predicate<ListViewModel> match)
        {
            List<ListViewModel> removing = new();
            foreach (var lvm in this)
            {
                if (match(lvm))
                {
                    removing.Add(lvm);
                }
            }

            foreach (var lvm in removing)
            {
                this.Remove(lvm);
            }

            return removing.Count;
        }

        public new ListViewModel this[int index]
        {
            get
            {
                return base[index];
            }
            set
            {
                base[index] = value;
                Reseed();
            }
        }

        private void Reseed()
        {
            foreach (var lvm in this)
            {
                lvm.Id = this.IndexOf(lvm);
            }
        }

    }
}
