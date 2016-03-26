using System;
using System.Collections.Generic;
using System.Threading;
using ChirpLib.IrcClient.EventArgs;
using ChirpLib.Utilities;

namespace ChirpLib.IrcClient
{
    public class IrcMessageHandler
    {
        #region Events
        public event EventHandler<IrcMessageEventArgs> OnPingMessage;
        public event EventHandler<IrcMessageEventArgs> OnReplyWelcome;
        public event EventHandler<IrcMessageEventArgs> OnReplyYourHost;
        public event EventHandler<IrcMessageEventArgs> OnReplyCreated;
        public event EventHandler<IrcMessageEventArgs> OnReplyMyInfo;
        public event EventHandler<IrcMessageEventArgs> OnReplyBounce;
        public event EventHandler<IrcMessageEventArgs> OnReplyUserHost;
        public event EventHandler<IrcMessageEventArgs> OnReplyIsOn;
        public event EventHandler<IrcMessageEventArgs> OnReplyAway;
        public event EventHandler<IrcMessageEventArgs> OnReplyUnaway;
        public event EventHandler<IrcMessageEventArgs> OnReplyNowAway;
        public event EventHandler<IrcMessageEventArgs> OnReplyWhoIsUser;
        public event EventHandler<IrcMessageEventArgs> OnReplyWhoIsServer;
        public event EventHandler<IrcMessageEventArgs> OnReplyWhoIsOperator;
        public event EventHandler<IrcMessageEventArgs> OnReplyWhoIsIdle;
        public event EventHandler<IrcMessageEventArgs> OnReplyEndOfWhoIs;
        public event EventHandler<IrcMessageEventArgs> OnReplyWhoIsChannels;
        public event EventHandler<IrcMessageEventArgs> OnReplyWhoWasUser;
        public event EventHandler<IrcMessageEventArgs> OnReplyEndOfWhoWas;
        public event EventHandler<IrcMessageEventArgs> OnReplyListStart;
        public event EventHandler<IrcMessageEventArgs> OnReplyList;
        public event EventHandler<IrcMessageEventArgs> OnReplyListEnd;
        public event EventHandler<IrcMessageEventArgs> OnReplyUniquOpIs;
        public event EventHandler<IrcMessageEventArgs> OnReplyChannelModeIs;
        public event EventHandler<IrcMessageEventArgs> OnReplyNoTopic;
        public event EventHandler<IrcMessageEventArgs> OnReplyTopic;
        public event EventHandler<IrcMessageEventArgs> OnReplyInviting;
        public event EventHandler<IrcMessageEventArgs> OnReplySummoning;
        public event EventHandler<IrcMessageEventArgs> OnReplyInviteList;
        public event EventHandler<IrcMessageEventArgs> OnReplyEndOfInviteList;
        public event EventHandler<IrcMessageEventArgs> OnReplyExceptList;
        public event EventHandler<IrcMessageEventArgs> OnReplyEndOfExceptList;
        public event EventHandler<IrcMessageEventArgs> OnReplyVersion;
        public event EventHandler<IrcMessageEventArgs> OnReplyWhoReply;
        public event EventHandler<IrcMessageEventArgs> OnReplyEndOfWho;
        public event EventHandler<IrcMessageEventArgs> OnReplyNameReply;
        public event EventHandler<IrcMessageEventArgs> OnReplyEndOfNames;
        public event EventHandler<IrcMessageEventArgs> OnReplyLinks;
        public event EventHandler<IrcMessageEventArgs> OnReplyEndOfLinks;
        public event EventHandler<IrcMessageEventArgs> OnReplyBanlist;
        public event EventHandler<IrcMessageEventArgs> OnReplyEndOfBanList;
        public event EventHandler<IrcMessageEventArgs> OnReplyInfo;
        public event EventHandler<IrcMessageEventArgs> OnReplyEndOfInfo;
        public event EventHandler<IrcMessageEventArgs> OnReplyMotdStart;
        public event EventHandler<IrcMessageEventArgs> OnReplyMotd;
        public event EventHandler<IrcMessageEventArgs> OnReplyEndOfMotd;
        public event EventHandler<IrcMessageEventArgs> OnReplyYourOper;
        public event EventHandler<IrcMessageEventArgs> OnReplyRehashing;
        public event EventHandler<IrcMessageEventArgs> OnReplyYourService;
        public event EventHandler<IrcMessageEventArgs> OnReplyTime;
        public event EventHandler<IrcMessageEventArgs> OnReplyUserStart;
        public event EventHandler<IrcMessageEventArgs> OnReplyUsers;
        public event EventHandler<IrcMessageEventArgs> OnReplyEndOfUsers;
        public event EventHandler<IrcMessageEventArgs> OnReplyNoUsers;
        public event EventHandler<IrcMessageEventArgs> OnReplyTraceLink;
        public event EventHandler<IrcMessageEventArgs> OnReplyTraceConnecting;
        public event EventHandler<IrcMessageEventArgs> OnReplyTracehandshake;
        public event EventHandler<IrcMessageEventArgs> OnReplyTraceUnknown;
        public event EventHandler<IrcMessageEventArgs> OnReplyTraceOperator;
        public event EventHandler<IrcMessageEventArgs> OnReplyTraceUser;
        public event EventHandler<IrcMessageEventArgs> OnReplyTraceServer;
        public event EventHandler<IrcMessageEventArgs> OnReplyTraceService;
        public event EventHandler<IrcMessageEventArgs> OnReplyTraceNewType;
        public event EventHandler<IrcMessageEventArgs> OnReplyTraceClass;
        public event EventHandler<IrcMessageEventArgs> OnReplyTraceConnect;
        public event EventHandler<IrcMessageEventArgs> OnReplyTraceLog;
        public event EventHandler<IrcMessageEventArgs> OnReplyTraceEnd;
        public event EventHandler<IrcMessageEventArgs> OnReplyStatsLinkInfo;
        public event EventHandler<IrcMessageEventArgs> OnReplyStatsCommands;
        public event EventHandler<IrcMessageEventArgs> OnReplyEndOfStats;
        public event EventHandler<IrcMessageEventArgs> OnReplyStatsUptime;
        public event EventHandler<IrcMessageEventArgs> OnReplyStatsOnline;
        public event EventHandler<IrcMessageEventArgs> OnReplyUModeIs;
        public event EventHandler<IrcMessageEventArgs> OnReplyServList;
        public event EventHandler<IrcMessageEventArgs> OnReplyServListEnd;
        public event EventHandler<IrcMessageEventArgs> OnReplyLUserClient;
        public event EventHandler<IrcMessageEventArgs> OnReplyLUserOp;
        public event EventHandler<IrcMessageEventArgs> OnReplyLUserUnknown;
        public event EventHandler<IrcMessageEventArgs> OnReplyLUserChannels;
        public event EventHandler<IrcMessageEventArgs> OnReplyLUserMe;
        public event EventHandler<IrcMessageEventArgs> OnReplyAdminMe;
        public event EventHandler<IrcMessageEventArgs> OnReplyAdminLoc1;
        public event EventHandler<IrcMessageEventArgs> OnReplyAdminLoc2;
        public event EventHandler<IrcMessageEventArgs> OnReplyAdminEmail;
        public event EventHandler<IrcMessageEventArgs> OnReplyTryAgain;
        public event EventHandler<IrcMessageEventArgs> OnErrorNoSuchNick;
        public event EventHandler<IrcMessageEventArgs> OnErrorNoSuchServer;
        public event EventHandler<IrcMessageEventArgs> OnErrorNoSuchChannel;
        public event EventHandler<IrcMessageEventArgs> OnErrorCannotSendToChan;
        public event EventHandler<IrcMessageEventArgs> OnErrorTooManyChannels;
        public event EventHandler<IrcMessageEventArgs> OnErrorWasNoSuchNick;
        public event EventHandler<IrcMessageEventArgs> OnErrorTooManyTargets;
        public event EventHandler<IrcMessageEventArgs> OnErrorNoSuchService;
        public event EventHandler<IrcMessageEventArgs> OnErrorNoOrigin;
        public event EventHandler<IrcMessageEventArgs> OnErrorNoRecipient;
        public event EventHandler<IrcMessageEventArgs> OnErrorNoTextToSend;
        public event EventHandler<IrcMessageEventArgs> OnErrorNoTopLevel;
        public event EventHandler<IrcMessageEventArgs> OnErrorWildTopLevel;
        public event EventHandler<IrcMessageEventArgs> OnErrorBadMask;
        public event EventHandler<IrcMessageEventArgs> OnErrorUnknownCommand;
        public event EventHandler<IrcMessageEventArgs> OnErrorNoMotd;
        public event EventHandler<IrcMessageEventArgs> OnErrorNoAdminInfo;
        public event EventHandler<IrcMessageEventArgs> OnErrorFileError;
        public event EventHandler<IrcMessageEventArgs> OnErrorNoNicknameGiven;
        public event EventHandler<IrcMessageEventArgs> OnErrorErroneusNickname;
        public event EventHandler<IrcMessageEventArgs> OnErrorNicknameInUse;
        public event EventHandler<IrcMessageEventArgs> OnErrorNickCollision;
        public event EventHandler<IrcMessageEventArgs> OnErrorUnavailResource;
        public event EventHandler<IrcMessageEventArgs> OnErrorUserNotInChannel;
        public event EventHandler<IrcMessageEventArgs> OnErrorNotOnChannel;
        public event EventHandler<IrcMessageEventArgs> OnErrorUserOnChannel;
        public event EventHandler<IrcMessageEventArgs> OnErrorNoLogin;
        public event EventHandler<IrcMessageEventArgs> OnErrorSummonDisabled;
        public event EventHandler<IrcMessageEventArgs> OnErrorUserDisabled;
        public event EventHandler<IrcMessageEventArgs> OnErrorNotRegistered;
        public event EventHandler<IrcMessageEventArgs> OnErrorNeedMoreParams;
        public event EventHandler<IrcMessageEventArgs> OnErrorAlreadyRegistered;
        public event EventHandler<IrcMessageEventArgs> OnErrorNoPermForHost;
        public event EventHandler<IrcMessageEventArgs> OnErrorPasswdMismatch;
        public event EventHandler<IrcMessageEventArgs> OnErrorYoureBannedCreep;
        public event EventHandler<IrcMessageEventArgs> OnErrorYouWillBeBanned;
        public event EventHandler<IrcMessageEventArgs> OnErrorKeyset;
        public event EventHandler<IrcMessageEventArgs> OnErrorChannelIsFull;
        public event EventHandler<IrcMessageEventArgs> OnErrorUnknownMode;
        public event EventHandler<IrcMessageEventArgs> OnErrorInviteOnlyChan;
        public event EventHandler<IrcMessageEventArgs> OnErrorBannedFromChan;
        public event EventHandler<IrcMessageEventArgs> OnErrorBadChannelKey;
        public event EventHandler<IrcMessageEventArgs> OnErrorBadChanMask;
        public event EventHandler<IrcMessageEventArgs> OnErrorNoChanModes;
        public event EventHandler<IrcMessageEventArgs> OnErrorBanListFull;
        public event EventHandler<IrcMessageEventArgs> OnErrorNoPrivileges;
        public event EventHandler<IrcMessageEventArgs> OnErrorChanOPrivsNeeded;
        public event EventHandler<IrcMessageEventArgs> OnErrorCantKillServer;
        public event EventHandler<IrcMessageEventArgs> OnErrorRestricted;
        public event EventHandler<IrcMessageEventArgs> OnErrorUniqOpPrivsNeeded;
        public event EventHandler<IrcMessageEventArgs> OnErrorNoOperHost;
        public event EventHandler<IrcMessageEventArgs> OnErrorUModeUnknownFlag;
        public event EventHandler<IrcMessageEventArgs> OnErrorUsersDontMatch;

        #endregion
       
        private static Dictionary<string, Action<IrcClient, IrcMessage>> MessageFactory = new Dictionary<string, Action<IrcClient, IrcMessage>>();
        private static Mutex mut = new Mutex();

        /// <summary>
        /// Loads the handlers.
        /// </summary>
        internal void LoadHandlers()
        {
            #region Handlers
            MessageFactory.Add("PING", OnPingMessageReceived);
            MessageFactory.Add("001", (IrcClient client, IrcMessage message) =>
                {
                    OnReplyWelcome?.ParallelInvoke(this, new IrcMessageEventArgs(client, message));
                });
            MessageFactory.Add("002", (IrcClient client, IrcMessage message) =>
                {
                    OnReplyYourHost?.ParallelInvoke(this, new IrcMessageEventArgs(client, message));
                });
            MessageFactory.Add("003", (IrcClient client, IrcMessage message) =>
                {
                    OnReplyCreated?.ParallelInvoke(this, new IrcMessageEventArgs(client, message));
                });
            MessageFactory.Add("004", (IrcClient client, IrcMessage message) =>
                {
                    OnReplyMyInfo?.ParallelInvoke(this, new IrcMessageEventArgs(client, message));
                });
            MessageFactory.Add("005", (IrcClient client, IrcMessage message) =>
                {
                    OnReplyBounce?.ParallelInvoke(this, new IrcMessageEventArgs(client, message));
                });
            MessageFactory.Add("302", (IrcClient client, IrcMessage message) =>
                {
                    OnReplyUserHost?.ParallelInvoke(this, new IrcMessageEventArgs(client, message));
                });
            MessageFactory.Add("303", (IrcClient client, IrcMessage message) =>
                {
                    OnReplyIsOn?.ParallelInvoke(this, new IrcMessageEventArgs(client, message));
                });
            MessageFactory.Add("301", (IrcClient client, IrcMessage message) =>
                {
                    OnReplyAway?.ParallelInvoke(this, new IrcMessageEventArgs(client, message));
                });
            MessageFactory.Add("305", (IrcClient client, IrcMessage message) =>
                {
                    OnReplyUnaway?.ParallelInvoke(this, new IrcMessageEventArgs(client, message));
                });
            MessageFactory.Add("306", (IrcClient client, IrcMessage message) =>
                {
                    OnReplyNowAway?.ParallelInvoke(this, new IrcMessageEventArgs(client, message));
                });
            MessageFactory.Add("311", (IrcClient client, IrcMessage message) =>
                {
                    OnReplyWhoIsUser?.ParallelInvoke(this, new IrcMessageEventArgs(client, message));
                });
            MessageFactory.Add("312", (IrcClient client, IrcMessage message) =>
                {
                    OnReplyWhoIsServer?.ParallelInvoke(this, new IrcMessageEventArgs(client, message));
                });
            MessageFactory.Add("313", (IrcClient client, IrcMessage message) =>
                {
                    OnReplyWhoIsOperator?.ParallelInvoke(this, new IrcMessageEventArgs(client, message));
                });
            MessageFactory.Add("317", (IrcClient client, IrcMessage message) =>
                {
                    OnReplyWhoIsIdle?.ParallelInvoke(this, new IrcMessageEventArgs(client, message));
                });
            MessageFactory.Add("318", (IrcClient client, IrcMessage message) =>
                {
                    OnReplyEndOfWhoIs?.ParallelInvoke(this, new IrcMessageEventArgs(client, message));
                });
            MessageFactory.Add("319", (IrcClient client, IrcMessage message) =>
                {
                    OnReplyWhoIsChannels?.ParallelInvoke(this, new IrcMessageEventArgs(client, message));
                });
            MessageFactory.Add("314", (IrcClient client, IrcMessage message) =>
                {
                    OnReplyWhoWasUser?.ParallelInvoke(this, new IrcMessageEventArgs(client, message));
                });
            MessageFactory.Add("369", (IrcClient client, IrcMessage message) =>
                {
                    OnReplyEndOfWhoWas?.ParallelInvoke(this, new IrcMessageEventArgs(client, message));
                });
            MessageFactory.Add("321", (IrcClient client, IrcMessage message) =>
                {
                    OnReplyListStart?.ParallelInvoke(this, new IrcMessageEventArgs(client, message));
                });
            MessageFactory.Add("322", (IrcClient client, IrcMessage message) =>
                {
                    OnReplyList?.ParallelInvoke(this, new IrcMessageEventArgs(client, message));
                });
            MessageFactory.Add("323", (IrcClient client, IrcMessage message) =>
                {
                    OnReplyListEnd?.ParallelInvoke(this, new IrcMessageEventArgs(client, message));
                });
            MessageFactory.Add("325", (IrcClient client, IrcMessage message) =>
                {
                    OnReplyUniquOpIs?.ParallelInvoke(this, new IrcMessageEventArgs(client, message));
                });
            MessageFactory.Add("324", (IrcClient client, IrcMessage message) =>
                {
                    OnReplyChannelModeIs?.ParallelInvoke(this, new IrcMessageEventArgs(client, message));
                });
            MessageFactory.Add("331", (IrcClient client, IrcMessage message) =>
                {
                    OnReplyNoTopic?.ParallelInvoke(this, new IrcMessageEventArgs(client, message));
                });
            MessageFactory.Add("332", (IrcClient client, IrcMessage message) =>
                {
                    OnReplyTopic?.ParallelInvoke(this, new IrcMessageEventArgs(client, message));
                });
            MessageFactory.Add("341", (IrcClient client, IrcMessage message) =>
                {
                    OnReplyInviting?.ParallelInvoke(this, new IrcMessageEventArgs(client, message));
                });
            MessageFactory.Add("342", (IrcClient client, IrcMessage message) =>
                {
                    OnReplySummoning?.ParallelInvoke(this, new IrcMessageEventArgs(client, message));
                });
            MessageFactory.Add("346", (IrcClient client, IrcMessage message) =>
                {
                    OnReplyInviteList?.ParallelInvoke(this, new IrcMessageEventArgs(client, message));
                });
            MessageFactory.Add("347", (IrcClient client, IrcMessage message) =>
                {
                    OnReplyEndOfInviteList?.ParallelInvoke(this, new IrcMessageEventArgs(client, message));
                });
            MessageFactory.Add("348", (IrcClient client, IrcMessage message) =>
                {
                    OnReplyExceptList?.ParallelInvoke(this, new IrcMessageEventArgs(client, message));
                });
            MessageFactory.Add("349", (IrcClient client, IrcMessage message) =>
                {
                    OnReplyEndOfExceptList?.ParallelInvoke(this, new IrcMessageEventArgs(client, message));
                });
            MessageFactory.Add("351", (IrcClient client, IrcMessage message) =>
                {
                    OnReplyVersion?.ParallelInvoke(this, new IrcMessageEventArgs(client, message));
                });
            MessageFactory.Add("352", (IrcClient client, IrcMessage message) =>
                {
                    OnReplyWhoReply?.ParallelInvoke(this, new IrcMessageEventArgs(client, message));
                });
            MessageFactory.Add("315", (IrcClient client, IrcMessage message) =>
                {
                    OnReplyEndOfWho?.ParallelInvoke(this, new IrcMessageEventArgs(client, message));
                });
            MessageFactory.Add("353", (IrcClient client, IrcMessage message) =>
                {
                    OnReplyNameReply?.ParallelInvoke(this, new IrcMessageEventArgs(client, message));
                });
            MessageFactory.Add("366", (IrcClient client, IrcMessage message) =>
                {
                    OnReplyEndOfNames?.ParallelInvoke(this, new IrcMessageEventArgs(client, message));
                });
            MessageFactory.Add("364", (IrcClient client, IrcMessage message) =>
                {
                    OnReplyLinks?.ParallelInvoke(this, new IrcMessageEventArgs(client, message));
                });
            MessageFactory.Add("365", (IrcClient client, IrcMessage message) =>
                {
                    OnReplyEndOfLinks?.ParallelInvoke(this, new IrcMessageEventArgs(client, message));
                });
            MessageFactory.Add("367", (IrcClient client, IrcMessage message) =>
                {
                    OnReplyBanlist?.ParallelInvoke(this, new IrcMessageEventArgs(client, message));
                });
            MessageFactory.Add("368", (IrcClient client, IrcMessage message) =>
                {
                    OnReplyEndOfBanList?.ParallelInvoke(this, new IrcMessageEventArgs(client, message));
                });
            MessageFactory.Add("371", (IrcClient client, IrcMessage message) =>
                {
                    OnReplyInfo?.ParallelInvoke(this, new IrcMessageEventArgs(client, message));
                });
            MessageFactory.Add("374", (IrcClient client, IrcMessage message) =>
                {
                    OnReplyEndOfInfo?.ParallelInvoke(this, new IrcMessageEventArgs(client, message));
                });
            MessageFactory.Add("375", (IrcClient client, IrcMessage message) =>
                {
                    OnReplyMotdStart?.ParallelInvoke(this, new IrcMessageEventArgs(client, message));
                });
            MessageFactory.Add("372", (IrcClient client, IrcMessage message) =>
                {
                    OnReplyMotd?.ParallelInvoke(this, new IrcMessageEventArgs(client, message));
                });
            MessageFactory.Add("376", OnEndOfMotdReceived);
            MessageFactory.Add("381", (IrcClient client, IrcMessage message) =>
                {
                    OnReplyYourOper?.ParallelInvoke(this, new IrcMessageEventArgs(client, message));
                });
            MessageFactory.Add("382", (IrcClient client, IrcMessage message) =>
                {
                    OnReplyRehashing?.ParallelInvoke(this, new IrcMessageEventArgs(client, message));
                });
            MessageFactory.Add("383", (IrcClient client, IrcMessage message) =>
                {
                    OnReplyYourService?.ParallelInvoke(this, new IrcMessageEventArgs(client, message));
                });
            MessageFactory.Add("391", (IrcClient client, IrcMessage message) =>
                {
                    OnReplyTime?.ParallelInvoke(this, new IrcMessageEventArgs(client, message));
                });
            MessageFactory.Add("392", (IrcClient client, IrcMessage message) =>
                {
                    OnReplyUserStart?.ParallelInvoke(this, new IrcMessageEventArgs(client, message));
                });
            MessageFactory.Add("393", (IrcClient client, IrcMessage message) =>
                {
                    OnReplyUsers?.ParallelInvoke(this, new IrcMessageEventArgs(client, message));
                });
            MessageFactory.Add("394", (IrcClient client, IrcMessage message) =>
                {
                    OnReplyEndOfUsers?.ParallelInvoke(this, new IrcMessageEventArgs(client, message));
                });
            MessageFactory.Add("395", (IrcClient client, IrcMessage message) =>
                {
                    OnReplyNoUsers?.ParallelInvoke(this, new IrcMessageEventArgs(client, message));
                });
            MessageFactory.Add("200", (IrcClient client, IrcMessage message) =>
                {
                    OnReplyTraceLink?.ParallelInvoke(this, new IrcMessageEventArgs(client, message));
                });
            MessageFactory.Add("201", (IrcClient client, IrcMessage message) =>
                {
                    OnReplyTraceConnecting?.ParallelInvoke(this, new IrcMessageEventArgs(client, message));
                });
            MessageFactory.Add("202", (IrcClient client, IrcMessage message) =>
                {
                    OnReplyTracehandshake?.ParallelInvoke(this, new IrcMessageEventArgs(client, message));
                });
            MessageFactory.Add("203", (IrcClient client, IrcMessage message) =>
                {
                    OnReplyTraceUnknown?.ParallelInvoke(this, new IrcMessageEventArgs(client, message));
                });
            MessageFactory.Add("204", (IrcClient client, IrcMessage message) =>
                {
                    OnReplyTraceOperator?.ParallelInvoke(this, new IrcMessageEventArgs(client, message));
                });
            MessageFactory.Add("205", (IrcClient client, IrcMessage message) =>
                {
                    OnReplyTraceUser?.ParallelInvoke(this, new IrcMessageEventArgs(client, message));
                });
            MessageFactory.Add("206", (IrcClient client, IrcMessage message) =>
                {
                    OnReplyTraceServer?.ParallelInvoke(this, new IrcMessageEventArgs(client, message));
                });
            MessageFactory.Add("207", (IrcClient client, IrcMessage message) =>
                {
                    OnReplyTraceService?.ParallelInvoke(this, new IrcMessageEventArgs(client, message));
                });
            MessageFactory.Add("208", (IrcClient client, IrcMessage message) =>
                {
                    OnReplyTraceNewType?.ParallelInvoke(this, new IrcMessageEventArgs(client, message));
                });
            MessageFactory.Add("209", (IrcClient client, IrcMessage message) =>
                {
                    OnReplyTraceClass?.ParallelInvoke(this, new IrcMessageEventArgs(client, message));
                });
            MessageFactory.Add("210", (IrcClient client, IrcMessage message) =>
                {
                    OnReplyTraceConnect?.ParallelInvoke(this, new IrcMessageEventArgs(client, message));
                });
            MessageFactory.Add("261", (IrcClient client, IrcMessage message) =>
                {
                    OnReplyTraceLog?.ParallelInvoke(this, new IrcMessageEventArgs(client, message));
                });
            MessageFactory.Add("262", (IrcClient client, IrcMessage message) =>
                {
                    OnReplyTraceEnd?.ParallelInvoke(this, new IrcMessageEventArgs(client, message));
                });
            MessageFactory.Add("211", (IrcClient client, IrcMessage message) =>
                {
                    OnReplyStatsLinkInfo?.ParallelInvoke(this, new IrcMessageEventArgs(client, message));
                });
            MessageFactory.Add("212", (IrcClient client, IrcMessage message) =>
                {
                    OnReplyStatsCommands?.ParallelInvoke(this, new IrcMessageEventArgs(client, message));
                });
            MessageFactory.Add("219", (IrcClient client, IrcMessage message) =>
                {
                    OnReplyEndOfStats?.ParallelInvoke(this, new IrcMessageEventArgs(client, message));
                });
            MessageFactory.Add("242", (IrcClient client, IrcMessage message) =>
                {
                    OnReplyStatsUptime?.ParallelInvoke(this, new IrcMessageEventArgs(client, message));
                });
            MessageFactory.Add("243", (IrcClient client, IrcMessage message) =>
                {
                    OnReplyStatsOnline?.ParallelInvoke(this, new IrcMessageEventArgs(client, message));
                });
            MessageFactory.Add("221", (IrcClient client, IrcMessage message) =>
                {
                    OnReplyUModeIs?.ParallelInvoke(this, new IrcMessageEventArgs(client, message));
                });
            MessageFactory.Add("234", (IrcClient client, IrcMessage message) =>
                {
                    OnReplyServList?.ParallelInvoke(this, new IrcMessageEventArgs(client, message));
                });
            MessageFactory.Add("235", (IrcClient client, IrcMessage message) =>
                {
                    OnReplyServListEnd?.ParallelInvoke(this, new IrcMessageEventArgs(client, message));
                });
            MessageFactory.Add("251", (IrcClient client, IrcMessage message) =>
                {
                    OnReplyLUserClient?.ParallelInvoke(this, new IrcMessageEventArgs(client, message));
                });
            MessageFactory.Add("252", (IrcClient client, IrcMessage message) =>
                {
                    OnReplyLUserOp?.ParallelInvoke(this, new IrcMessageEventArgs(client, message));
                });
            MessageFactory.Add("253", (IrcClient client, IrcMessage message) =>
                {
                    OnReplyLUserUnknown?.ParallelInvoke(this, new IrcMessageEventArgs(client, message));
                });
            MessageFactory.Add("254", (IrcClient client, IrcMessage message) =>
                {
                    OnReplyLUserChannels?.ParallelInvoke(this, new IrcMessageEventArgs(client, message));
                });
            MessageFactory.Add("255", (IrcClient client, IrcMessage message) =>
                {
                    OnReplyLUserMe?.ParallelInvoke(this, new IrcMessageEventArgs(client, message));
                });
            MessageFactory.Add("256", (IrcClient client, IrcMessage message) =>
                {
                    OnReplyAdminMe?.ParallelInvoke(this, new IrcMessageEventArgs(client, message));
                });
            MessageFactory.Add("257", (IrcClient client, IrcMessage message) =>
                {
                    OnReplyAdminLoc1?.ParallelInvoke(this, new IrcMessageEventArgs(client, message));
                });
            MessageFactory.Add("258", (IrcClient client, IrcMessage message) =>
                {
                    OnReplyAdminLoc2?.ParallelInvoke(this, new IrcMessageEventArgs(client, message));
                });
            MessageFactory.Add("259", (IrcClient client, IrcMessage message) =>
                {
                    OnReplyAdminEmail?.ParallelInvoke(this, new IrcMessageEventArgs(client, message));
                });
            MessageFactory.Add("263", (IrcClient client, IrcMessage message) =>
                {
                    OnReplyTryAgain?.ParallelInvoke(this, new IrcMessageEventArgs(client, message));
                });
            MessageFactory.Add("401", (IrcClient client, IrcMessage message) =>
                {
                    OnErrorNoSuchNick?.ParallelInvoke(this, new IrcMessageEventArgs(client, message));
                });
            MessageFactory.Add("402", (IrcClient client, IrcMessage message) =>
                {
                    OnErrorNoSuchServer?.ParallelInvoke(this, new IrcMessageEventArgs(client, message));
                });
            MessageFactory.Add("403", (IrcClient client, IrcMessage message) =>
                {
                    OnErrorNoSuchChannel?.ParallelInvoke(this, new IrcMessageEventArgs(client, message));
                });
            MessageFactory.Add("404", (IrcClient client, IrcMessage message) =>
                {
                    OnErrorCannotSendToChan?.ParallelInvoke(this, new IrcMessageEventArgs(client, message));
                });
            MessageFactory.Add("405", (IrcClient client, IrcMessage message) =>
                {
                    OnErrorTooManyChannels?.ParallelInvoke(this, new IrcMessageEventArgs(client, message));
                });
            MessageFactory.Add("406", (IrcClient client, IrcMessage message) =>
                {
                    OnErrorWasNoSuchNick?.ParallelInvoke(this, new IrcMessageEventArgs(client, message));
                });
            MessageFactory.Add("407", (IrcClient client, IrcMessage message) =>
                {
                    OnErrorTooManyTargets?.ParallelInvoke(this, new IrcMessageEventArgs(client, message));
                });
            MessageFactory.Add("408", (IrcClient client, IrcMessage message) =>
                {
                    OnErrorNoSuchService?.ParallelInvoke(this, new IrcMessageEventArgs(client, message));
                });
            MessageFactory.Add("409", (IrcClient client, IrcMessage message) =>
                {
                    OnErrorNoOrigin?.ParallelInvoke(this, new IrcMessageEventArgs(client, message));
                });
            MessageFactory.Add("411", (IrcClient client, IrcMessage message) =>
                {
                    OnErrorNoRecipient?.ParallelInvoke(this, new IrcMessageEventArgs(client, message));
                });
            MessageFactory.Add("412", (IrcClient client, IrcMessage message) =>
                {
                    OnErrorNoTextToSend?.ParallelInvoke(this, new IrcMessageEventArgs(client, message));
                });
            MessageFactory.Add("413", (IrcClient client, IrcMessage message) =>
                {
                    OnErrorNoTopLevel?.ParallelInvoke(this, new IrcMessageEventArgs(client, message));
                });
            MessageFactory.Add("414", (IrcClient client, IrcMessage message) =>
                {
                    OnErrorWildTopLevel?.ParallelInvoke(this, new IrcMessageEventArgs(client, message));
                });
            MessageFactory.Add("415", (IrcClient client, IrcMessage message) =>
                {
                    OnErrorBadMask?.ParallelInvoke(this, new IrcMessageEventArgs(client, message));
                });
            MessageFactory.Add("421", (IrcClient client, IrcMessage message) =>
                {
                    OnErrorUnknownCommand?.ParallelInvoke(this, new IrcMessageEventArgs(client, message));
                });
            MessageFactory.Add("422", (IrcClient client, IrcMessage message) =>
                {
                    OnErrorNoMotd?.ParallelInvoke(this, new IrcMessageEventArgs(client, message));
                });
            MessageFactory.Add("423", (IrcClient client, IrcMessage message) =>
                {
                    OnErrorNoAdminInfo?.ParallelInvoke(this, new IrcMessageEventArgs(client, message));
                });
            MessageFactory.Add("424", (IrcClient client, IrcMessage message) =>
                {
                    OnErrorFileError?.ParallelInvoke(this, new IrcMessageEventArgs(client, message));
                });
            MessageFactory.Add("431", (IrcClient client, IrcMessage message) =>
                {
                    OnErrorNoNicknameGiven?.ParallelInvoke(this, new IrcMessageEventArgs(client, message));
                });
            MessageFactory.Add("432", (IrcClient client, IrcMessage message) =>
                {
                    OnErrorErroneusNickname?.ParallelInvoke(this, new IrcMessageEventArgs(client, message));
                });
            MessageFactory.Add("433", (IrcClient client, IrcMessage message) =>
                {
                    OnErrorNicknameInUse?.ParallelInvoke(this, new IrcMessageEventArgs(client, message));
                });
            MessageFactory.Add("436", (IrcClient client, IrcMessage message) =>
                {
                    OnErrorNickCollision?.ParallelInvoke(this, new IrcMessageEventArgs(client, message));
                });
            MessageFactory.Add("437", (IrcClient client, IrcMessage message) =>
                {
                    OnErrorUnavailResource?.ParallelInvoke(this, new IrcMessageEventArgs(client, message));
                });
            MessageFactory.Add("441", (IrcClient client, IrcMessage message) =>
                {
                    OnErrorUserNotInChannel?.ParallelInvoke(this, new IrcMessageEventArgs(client, message));
                });
            MessageFactory.Add("442", (IrcClient client, IrcMessage message) =>
                {
                    OnErrorNotOnChannel?.ParallelInvoke(this, new IrcMessageEventArgs(client, message));
                });
            MessageFactory.Add("443", (IrcClient client, IrcMessage message) =>
                {
                    OnErrorUserOnChannel?.ParallelInvoke(this, new IrcMessageEventArgs(client, message));
                });
            MessageFactory.Add("444", (IrcClient client, IrcMessage message) =>
                {
                    OnErrorNoLogin?.ParallelInvoke(this, new IrcMessageEventArgs(client, message));
                });
            MessageFactory.Add("445", (IrcClient client, IrcMessage message) =>
                {
                    OnErrorSummonDisabled?.ParallelInvoke(this, new IrcMessageEventArgs(client, message));
                });
            MessageFactory.Add("446", (IrcClient client, IrcMessage message) =>
                {
                    OnErrorUserDisabled?.ParallelInvoke(this, new IrcMessageEventArgs(client, message));
                });
            MessageFactory.Add("451", (IrcClient client, IrcMessage message) =>
                {
                    OnErrorNotRegistered?.ParallelInvoke(this, new IrcMessageEventArgs(client, message));
                });
            MessageFactory.Add("461", (IrcClient client, IrcMessage message) =>
                {
                    OnErrorNeedMoreParams?.ParallelInvoke(this, new IrcMessageEventArgs(client, message));
                });
            MessageFactory.Add("462", (IrcClient client, IrcMessage message) =>
                {
                    OnErrorAlreadyRegistered?.ParallelInvoke(this, new IrcMessageEventArgs(client, message));
                });
            MessageFactory.Add("463", (IrcClient client, IrcMessage message) =>
                {
                    OnErrorNoPermForHost?.ParallelInvoke(this, new IrcMessageEventArgs(client, message));
                });
            MessageFactory.Add("464", (IrcClient client, IrcMessage message) =>
                {
                    OnErrorPasswdMismatch?.ParallelInvoke(this, new IrcMessageEventArgs(client, message));
                });
            MessageFactory.Add("465", (IrcClient client, IrcMessage message) =>
                {
                    OnErrorYoureBannedCreep?.ParallelInvoke(this, new IrcMessageEventArgs(client, message));
                });
            MessageFactory.Add("466", (IrcClient client, IrcMessage message) =>
                {
                    OnErrorYouWillBeBanned?.ParallelInvoke(this, new IrcMessageEventArgs(client, message));
                });
            MessageFactory.Add("467", (IrcClient client, IrcMessage message) =>
                {
                    OnErrorKeyset?.ParallelInvoke(this, new IrcMessageEventArgs(client, message));
                });
            MessageFactory.Add("471", (IrcClient client, IrcMessage message) =>
                {
                    OnErrorChannelIsFull?.ParallelInvoke(this, new IrcMessageEventArgs(client, message));
                });
            MessageFactory.Add("472", (IrcClient client, IrcMessage message) =>
                {
                    OnErrorUnknownMode?.ParallelInvoke(this, new IrcMessageEventArgs(client, message));
                });
            MessageFactory.Add("473", (IrcClient client, IrcMessage message) =>
                {
                    OnErrorInviteOnlyChan?.ParallelInvoke(this, new IrcMessageEventArgs(client, message));
                });
            MessageFactory.Add("474", (IrcClient client, IrcMessage message) =>
                {
                    OnErrorBannedFromChan?.ParallelInvoke(this, new IrcMessageEventArgs(client, message));
                });
            MessageFactory.Add("475", (IrcClient client, IrcMessage message) =>
                {
                    OnErrorBadChannelKey?.ParallelInvoke(this, new IrcMessageEventArgs(client, message));
                });
            MessageFactory.Add("476", (IrcClient client, IrcMessage message) =>
                {
                    OnErrorBadChanMask?.ParallelInvoke(this, new IrcMessageEventArgs(client, message));
                });
            MessageFactory.Add("477", (IrcClient client, IrcMessage message) =>
                {
                    OnErrorNoChanModes?.ParallelInvoke(this, new IrcMessageEventArgs(client, message));
                });
            MessageFactory.Add("478", (IrcClient client, IrcMessage message) =>
                {
                    OnErrorBanListFull?.ParallelInvoke(this, new IrcMessageEventArgs(client, message));
                });
            MessageFactory.Add("481", (IrcClient client, IrcMessage message) =>
                {
                    OnErrorNoPrivileges?.ParallelInvoke(this, new IrcMessageEventArgs(client, message));
                });
            MessageFactory.Add("482", (IrcClient client, IrcMessage message) =>
                {
                    OnErrorChanOPrivsNeeded?.ParallelInvoke(this, new IrcMessageEventArgs(client, message));
                });
            MessageFactory.Add("483", (IrcClient client, IrcMessage message) =>
                {
                    OnErrorCantKillServer?.ParallelInvoke(this, new IrcMessageEventArgs(client, message));
                });
            MessageFactory.Add("484", (IrcClient client, IrcMessage message) =>
                {
                    OnErrorRestricted?.ParallelInvoke(this, new IrcMessageEventArgs(client, message));
                });
            MessageFactory.Add("485", (IrcClient client, IrcMessage message) =>
                {
                    OnErrorUniqOpPrivsNeeded?.ParallelInvoke(this, new IrcMessageEventArgs(client, message));
                });
            MessageFactory.Add("491", (IrcClient client, IrcMessage message) =>
                {
                    OnErrorNoOperHost?.ParallelInvoke(this, new IrcMessageEventArgs(client, message));
                });
            MessageFactory.Add("501", (IrcClient client, IrcMessage message) =>
                {
                    OnErrorUModeUnknownFlag?.ParallelInvoke(this, new IrcMessageEventArgs(client, message));
                });
            MessageFactory.Add("502", (IrcClient client, IrcMessage message) =>
                {
                    OnErrorUsersDontMatch?.ParallelInvoke(this, new IrcMessageEventArgs(client, message));
                });
            #endregion
        }
        /// <summary>
        /// Execute the specified handler.
        /// </summary>
        /// <param name="key">Key.</param>
        /// <param name="client">Client.</param>
        /// <param name="message">Message.</param>
        internal void Execute(string key, IrcClient client, IrcMessage message)
        {
            Action<IrcClient, IrcMessage> action;
            if (MessageFactory.TryGetValue(key, out action))
            {
                action.Invoke(client, message);
           
            }
        }
        /// <summary>
        /// Adds a custom handler to MessageHandler.
        /// </summary>
        /// <param name="key">Key.</param>
        /// <param name="handler">Handler.</param>
        public void AddHandler(string key, Action<IrcClient, IrcMessage> handler)
        {
            if (String.IsNullOrWhiteSpace(key))
                throw new ArgumentNullException(nameof(key), "Null, empty or whitespace.");
            if (handler == null)
                throw new ArgumentNullException(nameof(handler), "Null handler");
            mut.WaitOne();
            MessageFactory.Add(key, handler);
            mut.ReleaseMutex();
        }
        /// <summary>
        /// Removes a custom handler from MessageHandler.
        /// </summary>
        /// <param name="key">Key.</param>
        public void RemoveHandler(string key)
        {
            if (String.IsNullOrWhiteSpace(key))
                throw new ArgumentNullException(nameof(key), "Null, empty or whitespace.");
            mut.WaitOne();
            MessageFactory.Remove(key);
            mut.ReleaseMutex();
        }

        private void OnPingMessageReceived(IrcClient client, IrcMessage message)
        {
            if (!String.IsNullOrWhiteSpace(message.Trail))
            {
                client.Send($"PONG {message.Trail}");
            }
            OnPingMessage?.ParallelInvoke(this, new IrcMessageEventArgs(client, message));
        }

        private void OnEndOfMotdReceived(IrcClient client, IrcMessage message)
        {
            List<string> channelsWithKeys = new List<string>();
            List<string> channels = new List<string>();
            List<string> keys = new List<string>();
            if (client.Settings.AutoJoin)
            {
                foreach (string channel in client.Settings.Channels)
                {
                    if (channel.Contains(":"))
                    {
                        string[] split = channel.Split(':');
                        channelsWithKeys.Add(split[0]);
                        keys.Add(split[1]);
                    }
                    else
                    {
                        channels.Add(channel);
                    }
                }
            }
            string channelsWithKeysParsed = string.Join(",", channelsWithKeys.ToArray());
            string keysParsed = string.Join(",", keys.ToArray());
            string channelsParsed = string.Join(",", channels.ToArray());
            client.SendJoin($"{channelsWithKeysParsed},{channelsParsed}", keysParsed);
            OnReplyEndOfMotd?.ParallelInvoke(this, new IrcMessageEventArgs(client, message));
        }
    }
}

