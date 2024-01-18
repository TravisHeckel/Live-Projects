from django.shortcuts import render, redirect, get_object_or_404
from django.core.paginator import Paginator, EmptyPage, PageNotAnInteger
from django.shortcuts import render
from .models import addQuest
from .forms import createQuest
from django.http import HttpResponse
#Create the first webpage 
def SideQuest_home(request):
    return render(request, 'SideQuest/SQ_home.html') #This renders the home page

# Creates a form for the user to add new quests
def SideQuest_add(request):
    form = createQuest(data=request.POST or None)
    if request.method == 'POST': #checks to make sure this is sending information, not requesting
        if form.is_valid(): #This will check to make sure the inputs are being considered valid.
            form.save() #This saves a new instance to the database
            return redirect('SideQuest_view')
    content = {'form': form}
    return render(request, 'SideQuest/SQ_add.html', content) #This renders the addquest page and

# Creates a page that shows all database items. 
def SideQuest_view(request):
    item = addQuest.Quests.all() #fetching all post objects from database
    p = Paginator(item, 2) #creating a paginator object
    page_number = request.GET.get('page') # getting the desired page number from url
    try:
        page_obj = p.get_page(page_number) #returns the desired page object
    except PageNotAnInteger:
        page_obj = p.page(1) #if page_number is not an integer then assign the first page
    except EmptyPage:
        page_obj = p.page(p.num_pages) # if page is empty then return last page
    content = {'page_obj': page_obj}
    return render(request, 'SideQuest/SQ_view.html',content) # sending the page object to SQ.view.html

#Create page that views the extensive details of the quest
def SideQuest_details(request, pk):
    pk = int(pk)
    item = get_object_or_404(addQuest, pk=pk)
    return render(request, 'SideQuest/SQ_details.html', { 'item': item})

#creates the page that allows for editing of items
def SideQuest_edit(request, pk):
    pk = int(pk)
    item = get_object_or_404(addQuest, pk=pk)
    form = createQuest(data=request.POST or None, instance=item)
    if request.method == 'POST':
        if form.is_valid():
#            form2 = form.save(commit=False)
#            form2.save()
            form.save()
            return redirect('SideQuest_view')
    content ={ 'form': form, 'item': item}
    return render(request, 'SideQuest/SQ_edit.html', content)


# This will delete the selected quest
def SideQuest_delete(request, pk):
    pk = int(pk)
    item = get_object_or_404(addQuest, pk=pk)
    if request.method == 'POST':
        item.delete()
        return redirect('SideQuest_home')
    content={'item':item}
    return render(request, 'SideQuest/SQ_delete.html', content)

# This is confirmation that you wanted to delete the quest
def SideQuest_confirmation(request):
    if request.method == 'POST':
        form = createQuest(request.POST or None)
        if form.is_valid():
            form.delete()
            return redirect('SideQuest_home')
    else:
        return redirect('SideQuest_view')