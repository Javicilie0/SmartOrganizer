using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Xml.Linq;

namespace SmartOrganizer
{
    public class AIClassifier
    {
        private readonly HttpClient client = new HttpClient();

        public async Task<string> ClassifyFile(string fileName)
        {
            var requestBody = new
            {
                model = "mistral",
                prompt = $"You are a file organizer assistant. Classify the file name '{fileName}' into one of these categories: Games, Media, Software, Documents, Pictures or Skip. " +
             "Definitions: " +
             "Games: Any game or game launcher (e.g. Steam, Epic Games, Riot Client, Fortnite, Minecraft, etc). " +
             "Pictures: Image files (e.g. .jpg, .png, .gif, .bmp, .tiff, etc) or image editing software. " +
             "Media: Media players (e.g. VLC, PotPlayer, Windows Media Player, iTunes, Winamp, Plex, Kodi, Audacity, DaVinci Resolve, Adobe Premiere) or files with media extensions (.mp3, .mp4, .avi, .mkv, .flac, .wav, .mov, .jpg, .png, .gif, etc). " +
             "Software: General-purpose software, tools, or utilities (e.g. Visual Studio, OBS, Chrome, Notepad++, WinRAR, Discord, Driver Booster, Antivirus, Intel XTU, etc). " +
             "Documents: Microsoft Office files (Excel, Word, PowerPoint, Access, OneNote, Outlook), their shortcuts, and any document files (.doc, .docx, .xls, .xlsx, .xlsm, .csv, .ppt, .pptx, .pdf, .txt, .rtf, .md, .json, .xml, .html, etc). " +
             "Skip: Any browser (Chrome, Edge, Firefox, Opera, Brave, etc), Recycle Bin, or files that do not fit any category. " +
             "Return only one word: Games, Media, Software, Documents,Pictures or Skip. " +
             "Examples: 'Microsoft Excel.lnk' → Documents, 'Excel File.xlsx' → Documents, 'Steam.lnk' → Games, 'VLC Media Player.lnk' → Media, 'Mem Reduct.lnk' → Software, 'Chrome.lnk' → Skip, 'Recycle Bin.lnk' → Skip."
            };

            var content = new StringContent(JsonConvert.SerializeObject(requestBody), Encoding.UTF8, "application/json");

            try
            {
                var response = await client.PostAsync("http://localhost:11434/api/generate", content);
                var responseString = await response.Content.ReadAsStringAsync();

                var resultLines = responseString.Split('\n');
                foreach (var line in resultLines)
                {
                    if (line.Contains("\"response\""))
                    {
                        var obj = JsonConvert.DeserializeObject<dynamic>(line);
                        return obj.response.ToString().Trim();
                    }
                }
            }
            catch
            {
                return "Unknown";
            }

            return "Unknown";
        }

    }
}
