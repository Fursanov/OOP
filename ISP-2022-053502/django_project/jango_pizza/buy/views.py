from django.views.generic import ListView, DetailView, TemplateView


class BuyPage(TemplateView):
    template_name = "buy.html"
