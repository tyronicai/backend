﻿// Copyright (c) 2018 Valdis Iljuconoks.
// Permission is hereby granted, free of charge, to any person
// obtaining a copy of this software and associated documentation
// files (the "Software"), to deal in the Software without
// restriction, including without limitation the rights to use,
// copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the
// Software is furnished to do so, subject to the following
// conditions:
// The above copyright notice and this permission notice shall be
// included in all copies or substantial portions of the Software.
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
// EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES
// OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
// NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT
// HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY,
// WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING
// FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR
// OTHER DEALINGS IN THE SOFTWARE.

using System;
using System.Linq;
using DbLocalizationProvider.Abstractions;
using DbLocalizationProvider.Cache;
using DbLocalizationProvider.Commands;
using Microsoft.EntityFrameworkCore;

namespace DbLocalizationProvider.AspNetCore.Commands
{
    public class CreateOrUpdateTranslationHandler : ICommandHandler<CreateOrUpdateTranslation.Command>
    {
        public void Execute(CreateOrUpdateTranslation.Command command)
        {
            using(var db = new LanguageEntities())
            {
                var resource = db.LocalizationResources.Include(r => r.Translations).FirstOrDefault(r => r.ResourceKey == command.Key);

                if(resource == null)
                {
                    // TODO: return some status response obj
                    return;
                }

                var translation = resource.Translations.FirstOrDefault(t => t.Language == command.Language.Name);

                if(translation != null)
                {
                    // update existing translation
                    translation.Value = command.Translation;
                }
                else
                {
                    var newTranslation = new LocalizationResourceTranslation
                                         {
                                             Value = command.Translation,
                                             Language = command.Language.Name,
                                             ResourceId = resource.Id
                                         };

                    db.LocalizationResourceTranslations.Add(newTranslation);
                }

                resource.ModificationDate = DateTime.UtcNow;
                resource.IsModified = true;
                db.SaveChanges();
            }

            ConfigurationContext.Current.CacheManager.Remove(CacheKeyHelper.BuildKey(command.Key));
        }
    }
}
