/*****************************************************************************
/* Copyright (c) 2023 xanthalas.co.uk                                       */
/*                                                                          */
/* Author: Xanthalas                                                        */
/* Date  : July 2023                                                        */
/*                                                                          */
/*  This file is part of p2c                                                */
/*                                                                          */
/*  p2c (Pipe to Clipboard)  is free software: you can redistribute it and  */
/*  or modify it under the terms of the GNU General Public License as       */
/*  published by the Free Software Foundation, either version 3 of the      */
/*  License, or (at your option) any later version.                         */
/*                                                                          */
/*  p2c is distributed in the hope that it will be useful,                  */
/*  but WITHOUT ANY WARRANTY; without even the implied warranty of          */
/*  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the           */
/*  GNU General Public License for more details.                            */
/*                                                                          */
/*  You should have received a copy of the GNU General Public License       */
/*  along with PipeToClipboard.  If not, see <http://www.gnu.org/licenses/>.*/
/*                                                                          */
/****************************************************************************/

namespace p2c;

using System;
using System.Text;
using TextCopy;

public static class PipeToClipboard
{
    public static async Task Main(string[] args)
    {
        var filter = string.Empty;

        if (args.Length == 1)
        {
            if (args[0] == "-h" || args[0] == "--help" || args[0] == "/?")
            {
                var nl = Environment.NewLine;

                Console.WriteLine("PipeToClipboard v 1.0 (c) Xanthalas 2023");
                Console.WriteLine($"{nl}Reads stdin and copies the contents to the clipboard.{nl}");
                Console.WriteLine("Usage: command | p2c <filter>");
                Console.WriteLine("For example: dir | p2c .exe");
                return;
            }

            filter = args[0].ToLower();
        }

        var output = new StringBuilder(string.Empty);

        while (Console.ReadLine() is { } line)
        {
            if (filter.Length > 0)
            {
                if (line.ToLower().Contains(filter))
                {
                    output.AppendLine(line);
                }
            }
            else
            {
                output.AppendLine(line);
            }
        }

        var finalString = output.ToString();

        if (finalString.Length > 0)
        {
            await ClipboardService.SetTextAsync(finalString);
        }
    }
}