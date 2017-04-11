using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nez.UI;
using Microsoft.Xna.Framework;

namespace HospitalCeo.UI.Elements.UI_World
{
    public class StartGameMainMenu : OutOfGameScreenMenu
    {
        private TextButton continueGame, newGame, loadGame, settings, exit;

        protected override void AfterCreation()
        {
            AddButton("Continue Game", out continueGame);
            AddButton("New Game", out newGame);
            AddButton("Load Game", out loadGame);
            AddButton("Settings", out settings);
            AddButton("Exit", out exit);

            loadGame.setDisabled(true);
            settings.setDisabled(true);

            continueGame.onClicked += onClicked => { GameStateManager.StartGame(); };
            newGame.onClicked += onClicked => { GameStateManager.StartGame(); };
            exit.onClicked += onClicked => { GameStateManager.GAME_STATE = GameState.Exit; };
        }

        private void AddButton(string text, out TextButton button)
        {
            TextButton b = new TextButton(text, defaultSkin);
            table.add(b);
            table.row();

            button = b;
        }

        protected override Table CreateTable()
        {
            Table t = new Table();
            t.defaults().setPadTop(10).setMinWidth(170).setMinHeight(30);
            t.setFillParent(true).center();
            return t;
        }

        protected override Color GetSceneColor()
        {
            return new Color(38, 38, 38);
        }
    }
}
