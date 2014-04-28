NetGet
======

Simple application for downloading URLs and saving them into a file.

Published under MIT licence.


Usage:
NetGet is a commandline tool, simply open your CMD, navigate to the folder where you saved the NetGet and type: 
netget.exe -h 
This will display the following help message:

Argument list:
-h - Zobrazí tuto nápovědu
-url - URL která bude stažena
-o - Název výstupního souboru, do kterého bude uložen stažený obsah
-r - Opakovat stažení každých n vteřin (např.: -r 30)
-rn Vzestupně očíslovat soubory při opakovaném stažení

NetGet currently supports the following interface languages: English, Czech
If you want to translate NetGet into another language, simply copy the lang.resx file, translate it, name it lang.de-DE.resx (for german) and create a pull request.
