using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CocosSharp;

namespace tests
{
    public class SpriteOffsetAnchorFlip : SpriteTestDemo
    {
        public SpriteOffsetAnchorFlip()
        {
            CCSize s = CCApplication.SharedApplication.MainWindowDirector.WindowSizeInPoints;

			// we only need to define the actions once
			var flip = new CCFlipY(true);
			var flip_back = flip.Reverse();
			var delay = new CCDelayTime (1);
			var seq = new CCSequence(delay, flip, delay, flip_back);

            for (int i = 0; i < 3; i++)
            {
                CCSpriteFrameCache cache = CCApplication.SharedApplication.SpriteFrameCache;
                cache.AddSpriteFrames("animations/grossini.plist");
                cache.AddSpriteFrames("animations/grossini_gray.plist", "animations/grossini_gray");

                //
                // Animation using Sprite batch
                //
                CCSprite sprite = new CCSprite("grossini_dance_01.png");
                sprite.Position = (new CCPoint(s.Width / 4 * (i + 1), s.Height / 2));

                CCSprite point = new CCSprite("Images/r1");
                point.Scale = 0.25f;
                point.Position = sprite.Position;
                AddChild(point, 1);

                switch (i)
                {
                    case 0:
                        sprite.AnchorPoint = new CCPoint(0, 0);
                        break;
                    case 1:
                        sprite.AnchorPoint = (new CCPoint(0.5f, 0.5f));
                        break;
                    case 2:
                        sprite.AnchorPoint = new CCPoint(1, 1);
                        break;
                }

                point.Position = sprite.Position;

                var animFrames = new List<CCSpriteFrame>();
                string tmp = "";
                for (int j = 0; j < 14; j++)
                {
                    string temp = "";
                    if (j +1< 10)
                    {
                        temp = "0" + (j + 1);
                    }
                    else
                    {
                        temp = (j + 1).ToString();
                    }
                    tmp = string.Format("grossini_dance_{0}.png", temp);
                    CCSpriteFrame frame = cache[tmp];
                    animFrames.Add(frame);
                }

                CCAnimation animation = new CCAnimation(animFrames, 0.3f);
                sprite.RunAction(new CCRepeatForever (new CCAnimate (animation)));

                animFrames = null;

				sprite.RepeatForever (seq);

                AddChild(sprite, 0);
            }
        }

        public override string title()
        {
            return "Sprite offset + anchor + flip";
        }

        public override string subtitle()
        {
            return "issue #1078";
        }
    }
}
