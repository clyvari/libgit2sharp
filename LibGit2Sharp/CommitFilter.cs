﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace LibGit2Sharp
{
    /// <summary>
    ///   Criterias used to filter out and order the commits of the repository when querying its history.
    /// </summary>
    public class CommitFilter
    {
        /// <summary>
        ///   Initializes a new instance of <see cref = "LibGit2Sharp.Filter" />.
        /// </summary>
        public CommitFilter()
        {
            SortBy = GitSortOptions.Time;
            Since = "HEAD";
        }

        /// <summary>
        ///   The ordering stragtegy to use.
        ///   <para>
        ///     By default, the commits are shown in reverse chronological order.
        ///   </para>
        /// </summary>
        public GitSortOptions SortBy { get; set; }

        /// <summary>
        ///   A pointer to a commit object or a list of pointers to consider as starting points.
        ///   <para>
        ///     Can be either a <see cref = "string" /> containing the sha or reference canonical name to use,
        ///     a <see cref = "Branch" />, a <see cref = "Reference" />, a <see cref = "Commit" />, a <see cref = "Tag" />,
        ///     a <see cref = "TagAnnotation" />, an <see cref="ObjectId"/> or even a mixed collection of all of the above.
        ///     By default, the <see cref = "Repository.Head" /> will be used as boundary.
        ///   </para>
        /// </summary>
        public object Since { get; set; }

        internal IList<object> SinceList
        {
            get { return ToList(Since); }
        }

        /// <summary>
        ///   A pointer to a commit object or a list of pointers which will be excluded (along with ancestors) from the enumeration.
        ///   <para>
        ///     Can be either a <see cref = "string" /> containing the sha or reference canonical name to use,
        ///     a <see cref = "Branch" />, a <see cref = "Reference" />, a <see cref = "Commit" />, a <see cref = "Tag" />,
        ///     a <see cref = "TagAnnotation" />, an <see cref="ObjectId"/> or even a mixed collection of all of the above.
        ///   </para>
        /// </summary>
        public object Until { get; set; }

        internal IList<object> UntilList
        {
            get { return ToList(Until); }
        }

        private static IList<object> ToList(object obj)
        {
            var list = new List<object>();

            if (obj == null)
            {
                return list;
            }

            var types = new[]
                            {
                                typeof(string), typeof(ObjectId),
                                typeof(Commit), typeof(TagAnnotation),
                                typeof(Tag), typeof(Branch), typeof(DetachedHead),
                                typeof(Reference), typeof(DirectReference), typeof(SymbolicReference)
                            };

            if (types.Contains(obj.GetType()))
            {
                list.Add(obj);
                return list;
            }

            list.AddRange(((IEnumerable)obj).Cast<object>());
            return list;
        }
    }
}
