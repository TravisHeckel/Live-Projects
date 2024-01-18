from django import forms
from .models import addQuest

#This creates a form that will house the necessary inputs we need from the user.
class createQuest(forms.ModelForm):
    class Meta:
        model = addQuest
        fields = ['id','name', 'difficulty', 'location', 'npc', 'reward', 'steps', 'recommendation', 'progress']
