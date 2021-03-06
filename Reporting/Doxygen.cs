﻿//------------------------------------------------------------------------------
// <copyright file="Doxygen.cs" company="Zebedee Mason">
//     Copyright (c) 2016-2017 Zebedee Mason.
//
//      The author's copyright is expressed through the following notice, thus
//      giving effective rights to copy and use this software to anyone, as shown
//      in the license text.
//
//     NOTICE:
//      This software is released under the terms of the GNU Lesser General Public
//      license; a copy of the text has been released with this package (see file
//      LICENSE, where the license text also appears), and can be found on the
//      GNU web site, at the following address:
//
//           http://www.gnu.org/licenses/old-licenses/lgpl-2.1.html
//
//      Please refer to the license text for any license information. This notice
//      has to be considered part of the license, and should be kept on every copy
//      integral or modified, of the source files. The removal of the reference to
//      the license will be considered an infringement of the license itself.
// </copyright>
//------------------------------------------------------------------------------

namespace DeepEnds.Reporting
{
    using DeepEnds.Reporting;
    using DeepEnds.Core;

    using System.Collections.Generic;
    using System.Linq;

    public class Doxygen
    {
        private Dictionary<string, string> options;

        public Doxygen(Dictionary<string, string> options)
        {
            this.options = options;
        }

        public void Write(DeepEnds.Core.Dependencies dependencies, DeepEnds.Reporting.Assemble assembled)
        {
            var fileName = this.options["doxygen"];
            var reporter = new Reporter(this.options["doxygen"], this.options, dependencies, assembled);
            reporter.Link = "\\ref DeepEnds{0}";
            reporter.LinkExt = "<a href=\"{0}\">{1}</a>";
            reporter.LineBegin = "//! ";
            reporter.ListItem = "- {0}";
            reporter.RightArrow = "&rarr;";
            reporter.SectionBegin = "\\page DeepEnds{0} {1}";
            reporter.SubsectionBegin = "\\section DeepEnds{0} {1}";
            reporter.SubSubsectionBegin = "\\subsection DeepEnds{0} {1}";
            reporter.TableBegin = "<table>\n";
            reporter.TableEnd = "</table>\n";
            reporter.TableBodyBegin = "\n";
            reporter.TableBodyEnd = "\n";
            reporter.TableBodyItem = "<td{0}>{1} ";
            reporter.TableHeadBegin = "\n";
            reporter.TableHeadEnd = "\n";
            reporter.TableHeadItem = "<th{1}>{2} ";
            reporter.TableRowBegin = "<tr{1}>";
            reporter.TableRowEnd = "\n";

            var cpp = "*.dox;*.txt;*.doc;*.c;*.C;*.cc;*.CC;*.cxx;*.cpp;*.c++;*.ii;*.ixx;*.ipp;*.i++;*.inl;*.h;*.H;*.hh;*.HH;*.hxx;*.hpp;*.h++;*.mm;";
            var cs = "*.cs;";
            var d = "*.d;";
            var fortran = "*.f;*.for;*.f90;*.f95;*.f03;*.f08;";
            var idl = "*.idl;*.ddl;*.odl;";
            var java = "*.java;";
            var objectiveC = "*.m;*.M;";
            var php = "*.php;*.php4;*.php5;*.inc;*.phtml;";
            var python = "*.py;*.pyw;";
            var tcl = "*.tcl;";
            var vhdl = "*.vhd;*.vhdl;*.ucf;*.qsf;";

            var ext = string.Format("*{0};", System.IO.Path.GetExtension(fileName));
            ext = ext.ToLower();
            if (fortran.Contains(ext))
            {
                reporter.FileHeader = "!>\n";
                reporter.LineBegin = "!! ";
            }
            else if (python.Contains(ext) || tcl.Contains(ext))
            {
                reporter.FileHeader = "##\n";
                reporter.LineBegin = "# ";
            }
            else if (vhdl.Contains(ext))
            {
                reporter.LineBegin = "--! ";
            }
            else if (cpp.Contains(ext) || cs.Contains(ext) || d.Contains(ext) || idl.Contains(ext) || java.Contains(ext) || objectiveC.Contains(ext) || php.Contains(ext))
            {
            }

            reporter.Report(true, false);
        }
    }
}
