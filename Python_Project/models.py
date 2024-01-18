from django.db import models

DIFF_CHOICE = [
    ('Easy', 'Easy'),
    ('Medium', 'Medium'),
    ('Hard', 'Hard'),
]

PRO_CHOICE = [
    ('locating', 'locating'),
    ('Working on', 'Working on'),
    ('Completed', 'Completed'),
]

NPC_CHOICE = [
    ('Boss', 'Boss'),
    ('Minion', 'Minion'),
    ('Ally', 'Ally'),
]

# This model allows for the user to add a new Quest from the game.
class addQuest(models.Model):
    name = models.CharField(max_length=60, default="", blank=True, null=False)
    difficulty = models.CharField(max_length=60, default="", blank=True, null=False, choices=DIFF_CHOICE)
    location = models.CharField(max_length=60, default="", blank=True, null=False)
    npc = models.CharField(max_length=60, default="", blank=True, null=False, choices=NPC_CHOICE)
    reward = models.CharField(max_length=60, default="", blank=True, null=False)
    steps = models.TextField(max_length=300, default="", blank=True, null=False)
    recommendation = models.TextField(max_length=300, default="", blank=True, null=False)
    progress = models.CharField(max_length=60, default="", blank=True, null=False, choices=PRO_CHOICE)

    Quests = models.Manager()

    def __str__(self):
        return self.name

