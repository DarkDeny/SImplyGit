using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;

namespace SimplyGit.Controls {
    /// <summary>
    /// Represents customized grid with sorted items
    /// </summary>
    [ContentProperty(nameof(SortedChildren))]
    public class LabelPanel : Grid {
        /// <summary>
        /// Gets the children. Use it instead of Children property.
        /// Specify sort order using Tag property for each child item.
        /// </summary>
        public List<LabeledControl> SortedChildren { get; set; }

        static LabelPanel() {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(LabelPanel),
                new FrameworkPropertyMetadata(typeof(LabelPanel)));
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SortedStackPanel"/> class.
        /// </summary>
        public LabelPanel() {
            SortedChildren = new List<LabeledControl>();
            SetIsSharedSizeScope(this, true);
        }

        public override void EndInit() {
            base.EndInit();

            RowDefinitions.Clear();

            int index = 0;
            foreach (var row in SortedChildren) {
                RowDefinitions.Add(new RowDefinition{Height = GridLength.Auto});
                SetRow(row, index++);
                Children.Add(row);
            }
        }
    }
}