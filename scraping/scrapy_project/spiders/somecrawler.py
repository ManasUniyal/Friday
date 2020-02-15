import scrapy
import re
import tldextract
import urllib.request
import os
import sys
import json
import time
import requests
import codecs
#from database.db_func import add_answer, connect, disconnect
from json import dumps as stringify
from html import unescape as unescapeHTML
from traceback import format_exc
from config_backend import debug


class QuotesSpider(scrapy.Spider):
    name = "spider"

    def __init__(self, question="", subject="Physics", _id=0, *args, **kwargs):
        super(QuotesSpider, self).__init__(*args, **kwargs)

        self.question = question
        self.id = _id
        self.debug = debug
        self.subject = subject

        self.answer = {"answer": [], "domain": [], "success": 1}

    def closed(self, reason):
        self.writetheanswer(bool(len(self.answer["answer"])), "[NO ANSWERS]")

    def log_error(self, location, *args):
        error = format_exc()

        s = "[" + time.strftime("%d-%m %H:%M:%S") + " IST]   "
        s += "[ERRORS]   " + location + " \n" + error + "\n\n"

        if self.debug:
            self.log(s)
        else:
            with open(os.path.join(os.getcwd(), "error_logs", "scrapy.log"), "a+") as f:
                f.write(s)

    def start_requests(self): 
        try:
            self.log("[STARTED REQUESTS]")
            q = self.question
            if "notes" not in self.subject.lower() :
                self.urls = self.return_links(q, True)["link"][:5]
            else:
                self.urls = self.return_links(q, False)["link"][:5]

            self.log("[URLS]  " + str(self.urls) +
                    " ----- [LEN]  " + str(len(self.urls)))

            if len(self.urls) != 0:
                for url in self.urls:
                    website = tldextract.extract(url).domain
                    self.log_error(self.subject)
                    # if website == 'brainly':
                    #     yield scrapy.Request(url=url, callback=self.parsebrainly)
                    if website == 'askiitians' and self.isQuestion:
                        yield scrapy.Request(url=url, callback=self.parseaskiitians)
                    elif website == 'askiitians' and (not self.isQuestion) :
                        yield scrapy.Request(url=url, callback=self.parseaskiitiansnotes)
                    elif website == 'doubtnut':
                        yield scrapy.Request(url=url, callback=self.parsedoubtnut)
                    elif website == 'stackexchange':
                        yield scrapy.Request(url=url, callback=self.parsestackexchange)
                    elif website == 'sarthaks':
                        yield scrapy.Request(url=url, callback=self.parsesarthaks)

                # print(stringify(self.answer, indent=2))
                # self.writetheanswer(bool(len(self.answer["answer"])), "[NO ANSWERS]")
            else:
                self.answer["success"] = 0
                self.writetheanswer(False, "[NO LINKS FOUND]")
        except Exception as e:
            self.writetheanswer(False, f"[START_REQUESTS]")

    def return_links(self, user_query, isQuestion):
        try:
            self.link_to_be_parsed = {}
            self.isQuestion = isQuestion
            self.user_query = user_query
            websites = ["doubtnut.com", 
                                   "askiitians.com", "stackexchange.com", "sarthaks.com"]
            self.useful_domains = ["doubtnut",
                                   "askiitians", "stackexchange", "sarthaks"]
            user_query = user_query.replace("++", "+")
            list_user_query = user_query.split("+")[:13]
           
#
#           sorted_list_user_query = list(list_user_query)
#
#            sorted_list_user_query.sort(key=lambda x: len(x), reverse=True)
#            sorted_list_user_query = sorted_list_user_query[:13]
#            
#            i=0
#            while i < len(list_user_query) :
#               if list_user_query[i] not in sorted_list_user_query:
#                   list_user_query.pop(i)
#                i+=1
            
            user_query = '+'.join(list_user_query)+ "+site%3A" + "+OR+site%3A".join(websites)
            
#            self.log(f"[USER QUERY] {user_query} \n[ SORTED LIST USER QUERY]{sorted_list_user_query}")

            self.log("\n" + "-" * 50 + "\n[USER_QUERY]  " + str(user_query))
            if isQuestion:
                google_search = "https://www.google.com/search?q=" + user_query
                self.log("[GOOGLE_SEARCH]   " +
                         str(google_search) + "\nIS_QUESTION")
            else:
                google_search = "https://www.google.com/search?q=" + \
                    user_query + "+askiitians+revision+notes"
                self.log("[GOOGLE_SEARCH]   " +
                         str(google_search) + "\nNOT_QUESTION")
            self.rq = requests.get(google_search).text

            self.urls = re.findall(r'href=[\'"]?([^\'" >]+)', self.rq)

            self.default_username = "fax"

            self.link_to_be_parsed["username"] = self.default_username
            self.link_to_be_parsed["link"] = []
            self.link_to_be_parsed["domain"] = []

            # self.log(stringify(self.urls, indent=2))


            for url in self.urls:
                # self.log(f"[RETURN LINKS URL] {url}")

                self.urlx = url[7:url.find(';') - 4]

                if tldextract.extract(self.urlx).domain in self.useful_domains:
                    self.link_to_be_parsed["link"].append(str(self.urlx))
                    self.link_to_be_parsed["domain"].append(
                        str(tldextract.extract(self.urlx).domain))
                # self.log("[RETURNED LINKS]")
            self.log(f"[RETURNED LINKS] : {stringify(self.link_to_be_parsed, indent =  2)}")

            return self.link_to_be_parsed
        except Exception as e:
            self.writetheanswer(False, f"[RETURN_LINKS]")
            
            return self.link_to_be_parsed

    # def parsebrainly(self, response):
    #     try:
    #         ans = response.xpath(
    #             "//div[@class='sg-text js-answer-content brn-rich-content']").extract()

    #         imgsrc = self.convertLinks(response.xpath(
    #             "//section[@id='answers']//img[@title='Attachment']/@src").extract(), response.request.url)

    #         ans = self.janitor(ans)

    #         self.answer["domain"].append(["brainly", response.request.url])
    #         self.answer["success"] = 1
    #         if imgsrc:
    #             self.answer["answer"].append([*ans, *imgsrc])
    #         else:
    #             self.answer["answer"].append([*ans])
    #     except Exception as e:

    #         self.log_error("[BRAINLY]")

    def parseaskiitiansnotes(self, response):
        self.log("[ASKNOTES CALLED]")
        try:

            l = response.xpath('//div[@id="content"]/pre/*').extract()
            l = self.janitor(l)

            img = self.convertLinks(response.xpath(
                '//div[@id="content"]//p//img/@src').extract(), response.request.url)

            self.answer["domain"].append(["askiitans", response.request.url])
            self.answer["success"] = 1

            if type(img) != list:
                img = [img]

            if img:
                self.answer["answer"].append([*l, *img])
            else:
                self.answer["answer"].append([*l])
        except Exception as e:
            self.log_error("[ASK_IITIANS_NOTES]")

    def parseaskiitians(self, response: dict) -> None:
        self.log("[ASK IITIANS CALLED]")
        try:

            l = response.xpath(
                '//*[@id="rptAnswers_ctl01_pnlAnswer"]').extract()
            l = self.janitor(l)

            img = self.convertLinks(response.xpath(
                '//div[@id="rptAnswers_ctl01_pnlAnswer"]//img/@src').extract(), response.request.url)

            self.answer["domain"].append(["askiitans", response.request.url])
            self.answer["success"] = 1

            if type(img) != list:
                img = [img]

            if img:
                self.answer["answer"].append([*l, *img])
            else:
                self.answer["answer"].append([*l])

        except scrapy.exceptions.NotSupported:
            self.answer["answer"].append(["Please click the link below:", response.url])
            self.answer["domain"].append(
                ["ASK_IITIANS", response.request.url])
            self.answer["success"] = 1
        except Exception:
            self.log_error("[ASK_IITIANS]")


    def parsestackexchange(self, response):
        try:
            answer = response.xpath(
                "//div[@class='post-text']/p/text()").extract()
            links = self.convertLinks(response.xpath(
                "//div[@class='post-text']//a/@href").extract(), response.request.url)
            for i in range(len(answer)):
                answer[i] = answer[i].replace("$$", "$")

            self.answer["answer"].append([*answer, *links])
            self.answer["domain"].append(
                ["Stack Exchange", response.request.url])
            self.answer["success"] = 1
        except Exception as e:
            self.log_error("[STACK_EXCHANGE]")

    def parsedoubtnut(self, response):
        try:

            htmls = str(response.text)
            i_ans_text = htmls.find("Answer Text")

            htmls = htmls[i_ans_text:]

            p_indexes = [m.start() for m in re.finditer('</p>', htmls)][2]
            half_almost_answer = htmls[:p_indexes]
            ind_almost_answer = half_almost_answer[::-1].find('>')
            answer = half_almost_answer[-ind_almost_answer:]
            answer = self.janitor(answer)
            self.answer["success"] = 1

            self.answer["domain"].append(["doubtnut", response.request.url])
            self.answer["answer"].append([*answer])
        except Exception as e:
            self.log_error("[DOUBTNUT]")

    def parsesarthaks(self, response):
        try:
            ans = response.xpath(
                '//div[@class="qa-a-item-content qa-post-content"]/div[@itemprop="text"]/*').extract()

            # links = response.xpath(
            #     '//div[@class="qa-a-item-content qa-post-content"]/div[@itemprop="text"]/p//span/img/@src').extract()



            for i in range(len(ans)):
                regexSearchResult = re.search("src=[\"'](.*?)[\"']", ans[i])
                if regexSearchResult:
                    ans[i] = "link" + \
                        unescapeHTML(regexSearchResult.group()[5:-1])

            ans = self.janitor(ans)

            i = 0

            while i < len(ans):
                if ans[i] == "":
                    ans.pop(i)
                else:
                    i += 1

            self.answer["domain"].append(["Sarthaks", response.request.url])
            if len(ans) > 0:
                self.answer["answer"].append(ans)
                self.answer["success"] = 1

        except Exception as e:
            self.log_error("[SARTHAKS]")

    def janitor(self, html_list):
        try:

            if type(html_list) != list:
                html_list = [html_list]

            ans_list = []

            for raw_html in html_list:
                split_str = '##SPLIT##'

                cleantext = re.sub('<br/?>', split_str, raw_html)
                cleantext = re.sub('&lt;br&gt;', split_str, cleantext)
                cleantext = re.sub('<p.*?>', split_str, cleantext)
                cleantext = re.sub('<.*?>', ' ', cleantext)
                cleantext = re.sub('\\\\xa0', ' ', cleantext)

                cleantext = re.sub('\\\\[A-Za-z]', ' ', cleantext)
                cleantext = re.sub("\\n", split_str, cleantext)

                cleantext = str(cleantext).rstrip(" []'")
                cleantext = str(cleantext).lstrip("[] '")

                cleantext = unescapeHTML(cleantext)

                i = 0
                while i < len(cleantext) - 1:
                    if cleantext[i] == '$' and cleantext[i+1] != "$":
                        cleantext = cleantext[:i] + "$$" + cleantext[i+1:]
                        i += 2

                    i += 1

                i = 0
                spaceFound = False
                while i < len(cleantext):

                    if cleantext[i] == " " and spaceFound == False:
                        temp = i
                        spaceFound = True
                    if cleantext[i] != " " and spaceFound == True:
                        spaceFound = False
                        cleantext = cleantext[:temp] + " " + cleantext[i:]
                    i += 1

                l = cleantext.split(split_str)

                i = 0

                while i < len(l):
                    if l[i] == ' ' or l[i] == '':
                        l.pop(i)
                    i += 1

                ans_list += l

            return ans_list
        except Exception as e:
            self.log_error("[JANITOR]")

    def convertLinks(self, links, url):
        try:
            url = url[:9] + url[9:].split("/")[0]

            if type(links) != list:
                links = links.split(",")
            newLinks = []

            for i in range(len(links)):
                links.extend(links.pop(0).split(','))

            for link in links:
                if link[0] == "/":
                    link = url + link
                newLinks.append("link"+link)
            return newLinks
        except Exception as e:
            self.writetheanswer(False, f"[CONVERT_LINKS]: ")
    
    def findanswer(self,lst):
        a = []
        for l in lst:
            if len(l) < 2:
                continue
            if l[0] == 'Answer Text ':
                a = l[1:]
                break
        ans = str()
        for tmp in a:
            ans+=tmp
        output = open("answer.txt","w")
        print(ans,file = output)

    def writetheanswer(self, works, error="[NO ERROR]"):
        if works:
            #add_answer(stringify(self.answer), 1, self.id)
            #output = open("answer.txt","w")
            #print(self.answer['answer'],file = output)
            self.findanswer(self.answer['answer'])
        else:
            self.log_error(f"[WRITE_THE_ANSWER]  {error}")

            self.answer = {
                "answer": ['Couldn\'t fetch answer, please try again'],
                "domain": ['Error'],
                "success": 0
            }
            #add_answer(stringify(self.answer), -1, self.id)
            #print(self.answer)
