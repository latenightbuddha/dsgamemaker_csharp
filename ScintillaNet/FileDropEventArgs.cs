using System;

#nullable disable
namespace ScintillaNet;

public class FileDropEventArgs : EventArgs
{
  private string[] _fileNames;

  public string[] FileNames => this._fileNames;

  public FileDropEventArgs(string[] fileNames) => this._fileNames = fileNames;
}
