﻿using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Grillisoft.DotnetTools.NewRepo.Creators.Impl
{
    public class DirectoryBuildPropsCreator : CreatorBase
    {
        public const string Name = "Directory.Build.props";

        private readonly NewRepoOptions _options;
        private readonly ILogger<DirectoryBuildPropsCreator> _logger;

        public DirectoryBuildPropsCreator(
            NewRepoOptions options,
            ILogger<DirectoryBuildPropsCreator> logger)
            : base(options)
        {
            _options = options;
            _logger = logger;
        }

        public override async Task Create(CancellationToken cancellationToken)
        {
            var dirs = new Dictionary<DirectoryInfo, string>
            {
                { this.Root,  "Root"  },
                { this.Src,   "Src"   },
                { this.Tests, "Tests" }
            };

            foreach(var entry in dirs)
            {
                var content = await GetTemplateContent(entry.Value + Name);
                await this.CreateTextFile(entry.Key.File(Name), content, _logger);
            }
        }
    }
}
