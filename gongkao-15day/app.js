const plan=[
{d:1,t:'先看懂公考全景图',s:'建立公务员、事业单位、招录流程的整体认知',lessons:[['什么是公考','把“公考”先理解成围绕公务员、事业单位等公开招录考试的一整套备考与报考场景。你现在先学框架，不急着背细节。'],['先分三条主线','第一条是国考；第二条是内蒙古省考；第三条是内蒙古事业单位。后续所有知识都先放进这三条线里。'],['今天必须会','能用自己的话解释：国考、省考、事业单位不是一回事；公告、职位表/岗位表、笔试、面试是招录流程中的不同环节。']],checks:['说清国考、省考、事业单位是三条不同主线','知道公告不是岗位表本身','知道笔试之后通常还有后续环节']},
{d:2,t:'国考、省考、事业单位怎么区分',s:'先学稳定区别，不背具体年份政策',lessons:[['国考','国家公务员招录，职位由中央和国家机关及其直属机构等提供。重点先记“国家层面公务员招录”。'],['省考','各省区组织的公务员招录。你当前重点关注内蒙古省考。'],['事业单位','事业单位公开招聘，岗位类型、考试类别、招聘单位和条件结构与公务员招录不同。'],['今天必须会','别人问“这三个考试有什么区别”，你能从招录主体、岗位性质、考试体系三个方向解释。']],checks:['能区分国考和内蒙古省考','能说出事业单位与公务员招录不是同一体系','不把具体考试时间当成永久固定规则']},
{d:3,t:'招考流程与高频术语',s:'先掌握一条完整链路',lessons:[['典型流程','公告发布 → 查看职位/岗位表 → 报名 → 资格审核 → 缴费/确认 → 笔试 → 查成绩/进面 → 资格复审 → 面试 → 体检考察等。不同考试细节可能不同。'],['高频术语','应届、往届、学历、学位、专业、户籍、基层经历、资格证、进面、最低进面分，后面筛岗都会反复遇到。'],['今天必须会','看到一份公告时，先判断它处于招录流程哪一步，而不是只看标题。']],checks:['能复述招考基本链路','知道资格审核和资格复审不是同一个阶段','理解“进面分”是后续筛岗分析常用参考']},
{d:4,t:'国考入门',s:'建立国考报考咨询的基础框架',lessons:[['先看什么','拿到国考公告或职位表，先看招录机关、地区、学历、专业、身份限制和其他资格条件。'],['不要死记时间','国考每年时间安排以当年官方公告为准，学习工具只训练判断框架。'],['咨询思路','用户说“我能不能考国考”，先补齐学历、专业、毕业身份、地区接受度等信息，再判断。']],checks:['知道国考判断不能只看学历','知道具体时间必须看当年公告','会先问专业和身份等条件']},
{d:5,t:'内蒙古省考入门',s:'把省考放进你熟悉的本地场景',lessons:[['核心框架','内蒙古省考属于地方公务员招录。面对咨询，先看当年公告和职位表，再做条件匹配。'],['本地咨询常见维度','盟市/旗县接受度、学历、专业、毕业身份、户籍或其他限制，都是筛岗时要核对的维度。'],['今天必须会','不要把“想留呼市”直接等同于“只看呼市岗位”，还要结合机会、限制和竞争情况做选择。']],checks:['知道内蒙古省考属于公务员招录','会先确认地区接受范围','不把热门地区自动等同于最适合']},
{d:6,t:'内蒙古事业单位入门',s:'理解岗位类别和招聘条件',lessons:[['先识别岗位表','事业单位岗位表通常会包含招聘单位、岗位名称、招聘人数、学历、专业及其他条件。具体字段以官方表格为准。'],['考试类别意识','事业单位可能存在不同岗位类别和对应考试内容，后续要结合你提供的内部资料进一步校准。'],['今天必须会','看到岗位表时能快速找到：地区、单位、岗位、人数、学历、专业、其他限制。']],checks:['能说出岗位表最关键的筛选字段','知道考试类别需要结合具体公告','知道岗位条件以官方表为准']},
{d:7,t:'岗位表字段识别',s:'从“看表”进入“会筛”',lessons:[['六个先看字段','地区、单位/岗位、学历、专业、毕业身份、其他限制。实际工作中还可能涉及资格证、户籍、性别等。'],['不要只搜专业名','专业字段可能写专业类、具体专业或其他口径，不能简单做字符串相等判断。'],['今天必须会','给你一行岗位数据，你能指出哪些字段是硬条件，哪些是偏好条件。']],checks:['能区分硬性限制和个人偏好','知道专业不能只做字面匹配','会主动看备注和其他条件']},
{d:8,t:'筛岗五步法',s:'建立固定筛选顺序',lessons:[['第一步','先排硬门槛：学历、专业、毕业身份及公告明确限制。'],['第二步','再按地区接受度缩小范围。'],['第三步','再看岗位性质、单位、招录人数等。'],['第四步','有历史数据时，再参考往年最低进面分等信息，但不能当成今年结果保证。'],['第五步','最后形成“明确可报 + 为什么 + 风险提醒”，而不是只甩一串岗位。']],checks:['会先硬条件后偏好','不会把往年分数当成今年保证','能解释推荐原因']},
{d:9,t:'实际筛岗练习',s:'开始从表格到结论',lessons:[['练习结构','输入一个人的学历、专业、身份、地区偏好等信息，再对岗位表进行筛选。'],['输出标准','只输出明确符合的岗位；不确定的条件必须单独标注，不要伪装成确定结论。'],['今天必须会','每次筛岗都留下“条件 → 筛选 → 结果 → 风险点”的过程。']],checks:['能写出完整筛岗条件','能解释为什么某岗位被排除','知道不确定就标注待确认']},
{d:10,t:'课程体系认识',s:'等待导入你们内部课程资料',lessons:[['当前先占位','这一部分不应该由通用知识替代。等你把随心学、直通班及其他实际产品资料发来后，再按公司真实口径填充。'],['今天可以先做','先整理你听过的课程名称、价格、服务周期、适合人群和最大差异点。']],checks:['知道课程信息必须按内部真实资料学习','开始整理课程差异点']},
{d:11,t:'课程匹配',s:'从“背产品”到“按需求推荐”',lessons:[['核心原则','不是先介绍最贵或最熟的课，而是先确认考试目标、基础、时间、预算和服务需求。'],['当前占位','具体课程匹配规则等你提供内部培训资料后再补。']],checks:['会先问需求再推荐','知道课程匹配不能脱离真实产品口径']},
{d:12,t:'咨询开场与需求挖掘',s:'学会先问再答',lessons:[['五类必问信息','考什么、什么时候考、当前基础、报考条件、地区/岗位偏好。涉及课程时再补预算、学习方式等。'],['避免一上来讲课','用户问一个简单问题时，先解决问题，再逐步补齐信息。'],['今天必须会','能把一次咨询从“问题”转成“完整需求画像”。']],checks:['会先解决用户眼前问题','能主动补齐缺失信息','不会连续输出一大段产品介绍']},
{d:13,t:'异议处理',s:'先判断真实问题，再回应',lessons:[['异议不等于拒绝','“贵”“考虑考虑”“先自己学”可能是价格、信任、效果、时间或决策权问题。'],['处理顺序','确认 → 追问真实顾虑 → 回应证据/方案 → 给下一步动作。'],['今天必须会','不急着反驳，先搞清用户为什么这么说。']],checks:['会追问真实顾虑','不把所有异议都理解成价格问题','能给出明确下一步']},
{d:14,t:'综合模拟咨询',s:'不提示，完整接一遍',lessons:[['今天少学多练','随机选择一个学员场景，从开场开始，完成需求确认、报考判断、基础建议和下一步。'],['复盘四问','我漏问了什么？我判断依据是什么？哪句话太早？下一步是否明确？']],checks:['完成至少2次模拟咨询','每次都写复盘','能指出自己最薄弱的一环']},
{d:15,t:'综合考核',s:'检验15天后到底会了多少',lessons:[['四块考核','基础知识、考试类型与流程、筛岗、模拟咨询。'],['通过标准','先不追求“专家”，目标是能完成基础咨询并知道什么问题必须查资料。'],['下一阶段','总分低于80时，不继续堆新知识，先针对薄弱项再练7天。']],checks:['完成综合测试','完成1次完整筛岗','完成1次完整模拟咨询','写出下一阶段薄弱项']}
];

const bank=[
{q:'国考和内蒙古省考最核心的区别之一是什么？',a:['都是事业单位招聘','一个是国家层面公务员招录，一个是地方公务员招录','只区别在考试地点','完全一样'],c:1},
{q:'拿到一份岗位表，以下哪个更适合作为第一步？',a:['先看往年最低进面分','先排学历、专业、身份等硬条件','先选最热门地区','先看岗位名称好不好听'],c:1},
{q:'用户问“我能不能报这个岗位”，最不应该怎么做？',a:['核对学历','核对专业','不看备注直接回答能报','核对身份限制'],c:2},
{q:'往年最低进面分应该怎么用？',a:['保证今年一定一样','作为历史参考之一','决定所有筛岗结果','不用看其他条件'],c:1},
{q:'资格审核和资格复审的关系，哪项更合理？',a:['永远是同一个环节','属于招录不同阶段，具体以公告为准','都等于笔试','都等于体检'],c:1},
{q:'咨询时对方说“课程太贵了”，第一反应更应该是？',a:['立刻降价','先确认真实顾虑','反驳对方','结束咨询'],c:1},
{q:'筛岗结果更好的输出方式是什么？',a:['只发岗位编号','只说能报','给出明确可报岗位、原因和风险提醒','所有岗位都列出来'],c:2},
{q:'事业单位岗位表中的专业条件应该怎么理解？',a:['只看字面完全相同','结合官方专业口径和岗位要求核对','忽略专业','只看学历'],c:1},
{q:'用户说“我想留呼市”，下一步更合理的是？',a:['只看呼市岗位','确认是否接受周边或其他地区，再结合岗位机会筛选','告诉他一定不要去别处','直接推荐课程'],c:1},
{q:'学习具体报名时间时，最可靠的依据是？',a:['多年以前的经验','当年官方公告','短视频评论区','别人口头说法'],c:1},
{q:'一次基础咨询中，以下哪组信息最值得优先补齐？',a:['星座、爱好、手机品牌','考试目标、学历、专业、身份、地区偏好','收入和住址','社交账号'],c:1},
{q:'“应届/往届”在筛岗中为什么重要？',a:['只是称呼','部分岗位会设置毕业身份条件','只影响面试穿着','完全不影响'],c:1}
];

const glossary=[
['国考','国家公务员招录的常用简称。具体职位、条件、时间和流程以当年官方公告及职位表为准。'],['省考','省级行政区域组织的公务员招录常用简称。你当前重点关注内蒙古省考。'],['事业单位','事业单位公开招聘体系，和公务员招录不是同一套岗位体系。'],['公告','官方发布的招录/招聘总体规则文件，通常包含时间、流程、条件等。'],['职位表/岗位表','列出具体招录职位或招聘岗位及条件的表格，是筛岗核心资料。'],['行测','公务员考试中常见的行政职业能力测验简称，具体科目设置以考试公告为准。'],['申论','公务员考试中常见科目之一，具体考试要求以当年公告为准。'],['职测','事业单位等考试场景中常见简称，具体类别与考试内容以公告为准。'],['综应','综合应用能力的常见简称，具体类别与内容以公告为准。'],['资格审核','报名等阶段对报考资格进行核验的环节，具体方式以公告为准。'],['资格复审','通常发生在后续阶段的资格再次核验，材料和要求以公告为准。'],['进面','进入面试环节的常用说法。'],['最低进面分','往年某职位/岗位进入面试的最低分数参考，不能当作今年结果保证。'],['应届','毕业身份相关概念，具体认定口径必须看对应考试当年公告。'],['往届','非当前应届身份的常用说法，具体报考限制仍以岗位条件为准。']
];

const scenarios=[
{title:'大四工商管理，想留呼市',desc:'第一次准备考公，不知道国考、省考、事业单位该选哪个。',must:['考试目标','学历','专业','毕业身份','地区接受范围','当前基础']},
{title:'往届汉语言文学，考过两次',desc:'只想找相对稳一点的岗位，对热门地区竞争比较担心。',must:['历史考试经历','地区接受范围','岗位偏好','学历','专业','身份限制']},
{title:'只想筛事业单位岗位',desc:'用户不想听课程，只想知道自己能报哪些岗位。',must:['学历','专业','毕业身份','地区','其他限制','先解决筛岗问题']},
{title:'觉得课程贵',desc:'用户有考试目标，但一听价格就说“我再考虑考虑”。',must:['真实顾虑','当前基础','备考时间','是否对比机构','决策因素','下一步动作']}
];

const store={get:k=>JSON.parse(localStorage.getItem(k)||'null'),set:(k,v)=>localStorage.setItem(k,JSON.stringify(v))};
let currentDay=store.get('currentDay')||1;
let done=store.get('done')||{};
let scores=store.get('scores')||[];

function $(s){return document.querySelector(s)}
function $all(s){return [...document.querySelectorAll(s)]}

function init(){
  $('#todayDate').textContent=new Date().toLocaleDateString('zh-CN',{year:'numeric',month:'long',day:'numeric',weekday:'long'});
  renderHome(); renderLearn(); renderQuiz(); renderScenarios(); renderGlossary(); renderProgress();
  $all('.nav button').forEach(b=>b.onclick=()=>switchPage(b.dataset.page));
  $('#searchTerm').oninput=renderGlossary;
}

function switchPage(id){$all('.page').forEach(p=>p.classList.add('hide'));$('#'+id).classList.remove('hide');$all('.nav button').forEach(b=>b.classList.toggle('active',b.dataset.page===id));}

function renderHome(){
 const completed=Object.values(done).filter(Boolean).length;
 const pct=Math.round(completed/15*100);
 $('#overallPct').textContent=pct+'%';$('#overallBar').style.width=pct+'%';$('#completedDays').textContent=completed;
 $('#avgScore').textContent=scores.length?Math.round(scores.reduce((a,b)=>a+b,0)/scores.length):0;
 $('#currentDayNo').textContent=currentDay;
 const p=plan[currentDay-1];$('#currentTitle').textContent=p.t;$('#currentSummary').textContent=p.s;
 const grid=$('#dayGrid');grid.innerHTML='';plan.forEach(x=>{const el=document.createElement('div');el.className='day-card '+(x.d===currentDay?'active ':'')+(done[x.d]?'done':'');el.innerHTML=`<div class="num">DAY ${x.d}</div><h4>${x.t}</h4><p>${x.s}</p>`;el.onclick=()=>{currentDay=x.d;store.set('currentDay',currentDay);renderHome();renderLearn();switchPage('learn')};grid.appendChild(el)});
}

function renderLearn(){const p=plan[currentDay-1];$('#learnDay').textContent=`第${p.d}天 · ${p.t}`;$('#learnSummary').textContent=p.s;$('#lessonList').innerHTML=p.lessons.map(x=>`<div class="lesson"><h4>${x[0]}</h4><p>${x[1]}</p></div>`).join('');$('#checkList').innerHTML=p.checks.map((x,i)=>`<label class="check"><input type="checkbox" class="dayCheck" data-i="${i}"><span>${x}</span></label>`).join('');$('#dayDoneBtn').textContent=done[p.d]?'已完成 · 点击取消':'完成今天学习';$('#dayDoneBtn').onclick=()=>{done[p.d]=!done[p.d];store.set('done',done);if(done[p.d]&&currentDay<15){currentDay++;store.set('currentDay',currentDay)}renderHome();renderLearn();renderProgress()};}

function renderQuiz(){const box=$('#quizList');const qs=[...bank].sort(()=>Math.random()-.5).slice(0,10);box.innerHTML=qs.map((x,i)=>`<div class="q-card" data-c="${x.c}"><h4>${i+1}. ${x.q}</h4>${x.a.map((o,j)=>`<label class="option"><input type="radio" name="q${i}" value="${j}"> ${o}</label>`).join('')}</div>`).join('');$('#submitQuiz').onclick=()=>{let n=0;[...box.children].forEach((c,i)=>{const checked=c.querySelector('input:checked');if(checked&&+checked.value===+c.dataset.c)n++});const sc=n*10;scores.push(sc);store.set('scores',scores);$('#quizResult').classList.remove('hide');$('#quizResult').innerHTML=`本次得分：<strong>${sc}</strong> 分。${sc>=80?'达到当前基础训练通过线。':'建议回到“今日学习”复习薄弱知识。'}`;renderHome();renderProgress();};$('#refreshQuiz').onclick=renderQuiz;}

function renderScenarios(){const grid=$('#scenarioGrid');grid.innerHTML=scenarios.map((s,i)=>`<div class="scenario" data-i="${i}"><h4>${s.title}</h4><p>${s.desc}</p></div>`).join('');$all('.scenario').forEach(el=>el.onclick=()=>openScenario(+el.dataset.i));}

function openScenario(i){const s=scenarios[i];$('#scenarioWork').classList.remove('hide');$('#scenarioTitle').textContent=s.title;$('#scenarioDesc').textContent=s.desc;$('#scenarioAnswer').value='';$('#scenarioResult').classList.add('hide');$('#submitScenario').onclick=()=>{const text=$('#scenarioAnswer').value.trim();if(!text){alert('先写下你会怎么接待这个学员。');return;}let hit=0;const txt=text.toLowerCase();const keys=['学历','专业','应届','往届','地区','基础','考试','岗位','时间','顾虑','预算','下一步','公告'];s.must.forEach(k=>{if(txt.includes(k))hit++});const score=Math.min(100,45+hit*8+Math.min(15,Math.floor(text.length/80)*3));$('#scenarioResult').classList.remove('hide');$('#scenarioResult').innerHTML=`<strong>离线基础评分：${score}分</strong><br><span class="muted">当前版本只做规则化自检，不等于AI专业评分。建议重点检查：${s.must.join('、')}。</span>`;};$('#copyAiPrompt').onclick=()=>{const prompt=`你是一个严格的公考咨询陪练客户。场景：${s.title}。背景：${s.desc}。不要主动泄露全部信息，让我通过提问获取。和我进行一轮真实咨询，结束后按：信息收集、判断逻辑、表达清晰度、下一步推进四项各100分评分，并指出3个最大问题。`;navigator.clipboard.writeText(prompt).then(()=>alert('陪练提示词已复制，可粘贴到ChatGPT继续练。'));};}

function renderGlossary(){const q=($('#searchTerm')?.value||'').trim().toLowerCase();const list=glossary.filter(x=>!q||x[0].toLowerCase().includes(q)||x[1].toLowerCase().includes(q));$('#glossaryList').innerHTML=list.map(x=>`<div class="term"><h4>${x[0]}</h4><p>${x[1]}</p></div>`).join('');}

function renderProgress(){const completed=Object.values(done).filter(Boolean).length;const knowledge=Math.min(100,Math.round(completed/9*70)+(scores.length?Math.round(scores.reduce((a,b)=>a+b,0)/scores.length*.3):0));const filter=Math.min(100,Math.round(([7,8,9].filter(x=>done[x]).length/3)*100));const consult=Math.min(100,Math.round(([12,13,14,15].filter(x=>done[x]).length/4)*100));$('#progressBody').innerHTML=`<div class="scorebar"><div class="head"><span>基础知识</span><b>${knowledge}%</b></div><div class="track"><span style="width:${knowledge}%"></span></div></div><div class="scorebar"><div class="head"><span>筛岗能力</span><b>${filter}%</b></div><div class="track"><span style="width:${filter}%"></span></div></div><div class="scorebar"><div class="head"><span>咨询能力</span><b>${consult}%</b></div><div class="track"><span style="width:${consult}%"></span></div></div><div class="notice" style="margin-top:16px">当前是个人训练版。课程体系、具体考情和真实筛岗案例还需要用你的内部资料继续补齐。</div>`;}

document.addEventListener('DOMContentLoaded',init);