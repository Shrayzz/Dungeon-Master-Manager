const { app, BrowserWindow } = require('electron')
const path = require('path')
const fs = require('fs')

const createMainWindow = () => {
    const mainWindow = new BrowserWindow({
        width: 1000,
        height: 600,
        minWidth: 640,
        minHeight: 480,     
        fullscreenable: true,
        frame: false,
        icon: path.join(__dirname, '../Dungeon-Master-Manager.ico'),
        webPreferences: {
            nodeIntegration: true,
            contextIsolation: false,
        },
        autoHideMenuBar: true,
        titleBarStyle: "hidden",
        titleBarOverlay: {
            height: 40,
            color: "#00000000",
            symbolColor: "#ffffff",
        },
    })

    mainWindow.loadFile('src/html/index.html')
}

app.on('ready', () => {
    createMainWindow()
})
