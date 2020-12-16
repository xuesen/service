using System;
using System.Collections.Generic;
using System.ComponentModel.Composition.Hosting;
using System.ComponentModel.Composition.Primitives;
using System.IO;
using System.Linq;

namespace IIMes.Infrastructure.MEF
{
    public class RecursiveDirectoryCatalog : ComposablePartCatalog, INotifyComposablePartCatalogChanged, ICompositionElement
    {
        private AggregateCatalog _aggregateCatalog;
        private readonly string _path;

        public RecursiveDirectoryCatalog(string path)
            : this(path, "*.dll")
        {
        }

        public RecursiveDirectoryCatalog(string path, string searchPattern)
        {
            if (path == null) throw new ArgumentNullException("path");

            _path = path;

            Initialize(path, searchPattern);
        }

        public void Referesh()
        {
            foreach (var dirCatalog in _aggregateCatalog.Catalogs)
            {
                ((DirectoryCatalog)dirCatalog).Refresh();
            }
        }

        private static IEnumerable<string> GetFoldersRecursive(string path)
        {
            var result = new List<string> { path };
            foreach (var child in Directory.GetDirectories(path))
            {
                result.AddRange(GetFoldersRecursive(child));
            }

            return result;
        }

        private void Initialize(string path, string searchPattern)
        {
            var directoryCatalogs = GetFoldersRecursive(path).Select(dir => new DirectoryCatalog(dir, searchPattern));
            _aggregateCatalog = new AggregateCatalog();
            _aggregateCatalog.Changed += (o, e) =>
            {
                if (Changed != null)
                {
                    Changed(o, e);
                }
            };
            _aggregateCatalog.Changing += (o, e) =>
            {
                if (Changing != null)
                {
                    Changing(o, e);
                }
            };
            foreach (var catalog in directoryCatalogs)
            {
                _aggregateCatalog.Catalogs.Add(catalog);
            }
        }

        public override IQueryable<ComposablePartDefinition> Parts
        {
            get { return _aggregateCatalog.Parts; }
        }

        public event EventHandler<ComposablePartCatalogChangeEventArgs> Changed;

        public event EventHandler<ComposablePartCatalogChangeEventArgs> Changing;

        private string GetDisplayName()
        {
            return string.Format("{0} (RecusrivePath=\"{1}\")", new object[] { GetType().Name, _path });
        }

        public override string ToString()
        {
            return GetDisplayName();
        }

        string ICompositionElement.DisplayName
        {
            get { return GetDisplayName(); }
        }

        ICompositionElement ICompositionElement.Origin
        {
            get { return null; }
        }
    }
}
