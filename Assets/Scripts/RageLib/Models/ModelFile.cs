﻿/**********************************************************************\

 RageLib
 Copyright (C) 2008  Arushan/Aru <oneforaru at gmail.com>

 This program is free software: you can redistribute it and/or modify
 it under the terms of the GNU General Public License as published by
 the Free Software Foundation, either version 3 of the License, or
 (at your option) any later version.

 This program is distributed in the hope that it will be useful,
 but WITHOUT ANY WARRANTY; without even the implied warranty of
 MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 GNU General Public License for more details.

 You should have received a copy of the GNU General Public License
 along with this program.  If not, see <http://www.gnu.org/licenses/>.

\**********************************************************************/

using System;
using System.IO;
using RageLib.Models.Data;
using RageLib.Models.Resource;
using RageLib.Textures;
using UnityEngine;

namespace RageLib.Models
{
    public class ModelFile : IModelFile
    {
        internal File<DrawableModel> File { get; private set; }
        byte[] _data;

        public void Open(string filename)
        {
            File = new File<DrawableModel>();
            File.Open(filename);
        }

        public void Open(Stream stream)
        {
            File = new File<DrawableModel>();
            File.Open(stream);
        }

        public void Open(byte[] data)
        {
            File = new File<DrawableModel>();
            _data = data;
        }

        public void Read()
        {
            using (var stream = new MemoryStream(_data))
            {
                File.Open(stream);
            }
        }

        public TextureFile EmbeddedTextureFile
        {
            get { return File.Data.ShaderGroup.TextureDictionary; }
        }

        public ModelNode GetModel(TextureFile[] textures)
        {
            return ModelGenerator.GenerateModel(File.Data, textures);
        }

        public Drawable GetDataModel()
        {
            return new Drawable(File.Data);
        }

        #region Implementation of IDisposable

        public void Dispose()
        {
            if (File != null)
            {
                File.Dispose();
                File = null;
            }

        }

        #endregion
    }
}
