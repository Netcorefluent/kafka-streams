FROM node:10-slim

LABEL "repository"="http://github.com/netcorefluent/kafka-streams"
LABEL "homepage"="http://github.com/netcorefluent"
LABEL "maintainer"="Jacob Zweifel <jacob@jacobzweifel.com>"

LABEL "com.github.actions.name"="ECLint Wrapper"
LABEL "com.github.actions.description"="Wraps eclint to enable running eclint as an action."
LABEL "com.github.actions.icon"="code"
LABEL "com.github.actions.color"="yellow"

RUN yarn global add eclint

ADD entrypoint.sh /entrypoint.sh
ENTRYPOINT ["/entrypoint.sh"]
