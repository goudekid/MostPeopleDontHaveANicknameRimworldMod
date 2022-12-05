# MostPeopleDontHaveANicknameRimworldMod
AI Generated Harmony Patching Mod for Rimworld to allow you to control the probability a pawn with generate with a nickname


Most people don't go by a nickname. This mod adds a slider to it's mod settings where you can set what % of people will just go by their first name rather than by a nickname.


# Details on the AI thing below

A small issue I had with Rimworld was that nearly EVERYONE has a nickname. This bothered me in a pedantic way so I always sort of wanted to make a mod for this but it was always too much effort for such a small thing. But I was playing around with OpenAI's ChatGPT chatbot when I heard other say people were using it to generate code and well...

Let's say that 95% of this mod was not made by me. (The other 5% was due to all the data the mod trained on being from 2021 and prior, meaning some things changed under the hood in the rimworld DLLs that it didn't know about. Cleaning this up lead to the code the AI generated working perfectly with VERY minimal tweaking.)

The actual process went something like this.

I asked the bot "Can you make me a Rimworld mod using harmony patches to make it so that pawns generate with their nickname set to their first name 90% of the time and only have a real nickname the other 10%".

It said sure and whipped up something 80% correct. The harmony patches were being applied inline using a harmony instance (which wasnt actually instantiated anywhere) and it was also just kind of postfixing the .nick equal to the .first for the names. Also the method it was trying to harmony patch had it's signature change at some point between when the AI was trained and now so that was also wrong.

So I then asked it "Can you do the harmony patches as attributes rather than in the constructor?". It did it perfectly.
Then I told it "The signature for this method changed recently to "...." can you use that instead for the patch? And it did, perfectly.
Finally, I noticed that the new signature had a ForceNoNickname bool that might work better for this, so I asked it to use that instead for the logic. And it did.

I then pondered if it could also implement the required settings infra to allow the user the change the percent chances from 90% to anything so I asked it to implement a slider in the mod settings for that. It was mostly able to do this but was missing a small piece that I'm also chalking up to the changes since the AI was trained.

It's kinda crazy how well it worked. Legit a tiny bit worried about my career now, but maybe I can switch gears to "AI magician" instead of software engineer...
